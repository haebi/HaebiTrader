using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HaebiTrader
{
    public partial class HaebiMain : Form
    {
        // 계좌 통합잔고
        DataTable dtOverall = new DataTable();
        DataTable dtAccount = new DataTable();

        // 키움API 스크린 넘버
        int screenNo = 1100;

        public HaebiMain()
        {
            InitializeComponent();

            // 로그 출력
            txtLog.ReadOnly = true;

            // 키움 API 이벤트
            axKHOpenAPI1.OnEventConnect         += AxKHOpenAPI1_OnEventConnect;
            axKHOpenAPI1.OnReceiveTrData        += AxKHOpenAPI1_OnReceiveTrData;
            axKHOpenAPI1.OnReceiveChejanData    += AxKHOpenAPI1_OnReceiveChejanData;
            axKHOpenAPI1.OnReceiveRealData      += AxKHOpenAPI1_OnReceiveRealData;

            // 계좌 잔고정보 초기화
            initAccountOverall();
            initAccount();

            // 주문 콤보박스 세팅
            initOrderType();
            initTradeType();
        }

        // ----------------------------------------------------------------------------------------------------
        // [API] 실시간데이터 이벤트 처리
        // ----------------------------------------------------------------------------------------------------
        private void AxKHOpenAPI1_OnReceiveRealData(object sender, AxKHOpenAPILib._DKHOpenAPIEvents_OnReceiveRealDataEvent e)
        {
            string js_jongmok = e.sRealKey; // 키(종목코드)
            string js_data  =   e.sRealData; // 데이터

            if (e.sRealType.Equals("주식체결"))
            {
                string js_price_che = axKHOpenAPI1.GetCommRealData(e.sRealKey, 10); // 체결가

                // 종목 현재가 갱신
                // 잔고 조회할때는 앞에 A가 붙어 나오고 여기 현재가 데이터는 숫자만 나온다... 뒤에 숫자 6자리만 사용 고려...
                dtAccount.Select(string.Format("[C8] = 'A{0}'", js_jongmok))
                    .ToList<DataRow>()
                    .ForEach(r =>
                    {
                        // 현재가
                        updatePrice(r, Convert.ToInt64(js_price_che.Replace("+", "").Replace("-", "")));
                    });
            }
        }

        // ----------------------------------------------------------------------------------------------------
        // [API] 체결/잔고 이벤트 처리
        // ----------------------------------------------------------------------------------------------------
        private void AxKHOpenAPI1_OnReceiveChejanData(object sender, AxKHOpenAPILib._DKHOpenAPIEvents_OnReceiveChejanDataEvent e)
        {
            // 주문 넣으면 0, 1 이 같이 들어옴
            if (e.sGubun.Equals("0")) // 접수 or 체결
            {
                string account      = axKHOpenAPI1.GetChejanData(9201); // 계좌번호
                string orderNumber  = axKHOpenAPI1.GetChejanData(9203); // 주문번호
                string orderState   = axKHOpenAPI1.GetChejanData(913);  // 주문상태  접수  / 체결

                if (orderState.Equals("접수"))
                {
                    Log(string.Format("주문번호 : {0} {1}", orderNumber, orderState), "[API]");
                }
                else if (orderState.Equals("체결"))
                {
                    string jsName           = axKHOpenAPI1.GetChejanData(302);  // 종목명
                    string jsMMType         = axKHOpenAPI1.GetChejanData(907);  // 매도수구분 / 1:매도 2:매수
                    string jsCheQwantity    = axKHOpenAPI1.GetChejanData(910);  // 체결가
                    string jsChePrice       = axKHOpenAPI1.GetChejanData(911);  // 체결량

                    string jsOrderType      = axKHOpenAPI1.GetChejanData(905); // 주문구분 / +매수 -매도
                    string jsTradeType      = axKHOpenAPI1.GetChejanData(906); // 매매구분 / 시장가

                    Log(string.Format("{0} {1} 체결가 : {2} 체결량 : {3}", jsName.Trim(), jsOrderType, jsCheQwantity, jsChePrice), "[MM-CHE]");
                }
            }
            else if (e.sGubun.Equals("1")) // 잔고
            {
                string itemName         = axKHOpenAPI1.GetChejanData(302);
                string price            = axKHOpenAPI1.GetChejanData(10);
                string amount           = axKHOpenAPI1.GetChejanData(930);
                string buyingPrice      = axKHOpenAPI1.GetChejanData(931);
                string totalBuyingPrice = axKHOpenAPI1.GetChejanData(932);
            }
            else if (e.sGubun.Equals("4")) // 파생잔고
            {

            }
        }

        // ----------------------------------------------------------------------------------------------------
        // [API] TR 이벤트 처리
        // ----------------------------------------------------------------------------------------------------
        private void AxKHOpenAPI1_OnReceiveTrData(object sender, AxKHOpenAPILib._DKHOpenAPIEvents_OnReceiveTrDataEvent e)
        {
            if (e.sRQName.Equals("계좌평가잔고내역요청1"))
            {
                string total_buy            = axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, 0, "총매입금액");
                string total_evaluate       = axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, 0, "총평가금액");
                string total_profit         = axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, 0, "총평가손익금액");
                string total_profit_ratio   = axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, 0, "총수익률(%)");
                string total_assets         = axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, 0, "추정예탁자산");
                               
                dtOverall.Rows[0][1] = zTrimInt(total_buy);                 // 총매입
                dtOverall.Rows[1][1] = zTrimInt(total_evaluate);            // 총평가
                dtOverall.Rows[0][3] = zTrimInt(total_profit);              // 평가손익
                dtOverall.Rows[1][3] = zTrimFloat(total_profit_ratio);      // 수익률
                dtOverall.Rows[0][5] = strMinus(total_assets, total_buy);   // 추정자산
                dtOverall.Rows[1][5] = zTrimInt(total_assets);              // 매수가능액
            }
            else if (e.sRQName.Equals("계좌평가잔고내역요청2"))
            {
                int count = axKHOpenAPI1.GetRepeatCnt(e.sTrCode, e.sRQName);
                Log(string.Format("종목갯수 : {0}", count), "[API]");

                dtAccount.Rows.Clear();
                for (int i = 0; i < count; i++)
                {
                    //      0|       1|       2|     3|       4|     5|       6|      7
                    // 종목명|매입총액|평가손익|수익률|평균단가|현재가|보유수량|종목코드
                    string jm_name          = axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, i, "종목명");
                    string jm_cost_buy      = axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, i, "매입금액");
                    string jm_evaluate      = axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, i, "평가손익");
                    string jm_profit_ratio  = axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, i, "수익률(%)");
                    string jm_cost_buy_one  = axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, i, "매입가");
                    string jm_cost_now      = axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, i, "현재가");
                    string jm_qwantity      = axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, i, "보유수량");
                    string jm_code          = axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, i, "종목번호");

                    DataRow dr = dtAccount.NewRow();
                    dr[0] = jm_name.Trim();
                    dr[1] = Convert.ToInt64(jm_cost_buy);
                    dr[2] = Convert.ToInt64(jm_evaluate);
                    dr[3] = Convert.ToDouble(jm_profit_ratio);
                    dr[4] = Convert.ToInt64(jm_cost_buy_one);
                    dr[5] = Convert.ToInt64(jm_cost_now);
                    dr[6] = Convert.ToInt64(jm_qwantity);
                    dr[7] = jm_code.Trim(); // trim이 필요한지는 확인 필요.
                    dtAccount.Rows.Add(dr);
                }
            }
        }

        // ----------------------------------------------------------------------------------------------------
        // [API] 로그인 이벤트 처리
        // ----------------------------------------------------------------------------------------------------
        private void AxKHOpenAPI1_OnEventConnect(object sender, AxKHOpenAPILib._DKHOpenAPIEvents_OnEventConnectEvent e)
        {
            Log("키움증권 연결 중", "[LOGIN]");

            if (e.nErrCode == 0)
            {
                Log("키움증권 연결 성공", "[LOGIN]");

                string server_type = axKHOpenAPI1.GetLoginInfo("GetServerGubun"); // 1: 모의서버 or 실서버
                Log(string.Format("서버타입 : {0}", server_type), "[LOGIN]");

                Log("키움증권 계좌목록 조회", "[LOGIN]");

                string account_list = axKHOpenAPI1.GetLoginInfo("ACCLIST");
                string[] account = account_list.Split(';');
                
                for (int i = 0; i < account.Length; i++)
                {
                    if ( !"".Equals(account[i]) )
                        cboAccount.Items.Add(account[i]);
                }

                if (cboAccount.Items.Count > 0)
                {
                    cboAccount.SelectedIndex = 0;
                }

                Log(string.Format("계좌 개수 : {0} 건 조회 완료", account.Length -1), "[LOGIN]");
            }
            else
            {
                Log("키움증권 연결 실패", "[LOGIN]");
            }
        }

        // ----------------------------------------------------------------------------------------------------
        // 버튼 이벤트
        // ----------------------------------------------------------------------------------------------------
        private void Button_Click(object sender, EventArgs e)
        {
            // 로그인 -> AxKHOpenAPI1_OnEventConnect
            if (sender.Equals(btnLogin))
            {
                axKHOpenAPI1.CommConnect(); // AxKHOpenAPI1_OnEventConnect
            }
            else if (sender.Equals(btn1))
            {
                // 통합 잔고조회 -> AxKHOpenAPI1_OnReceiveTrData
                getAccountEvaluate("1"); // 통합
            }
            else if (sender.Equals(btn2))
            {
                // 종목별 잔고조회 -> AxKHOpenAPI1_OnReceiveTrData
                getAccountEvaluate("2"); // 종목별
            }
            else if (sender.Equals(btnOrder))
            {
                // 주문 -> AxKHOpenAPI1_OnReceiveChejanData()
                string order_type   = cboOrderType.Text;
                string itemCode     = txtStockCode.Text;
                int quantity        = (int)nudOrderQuantity.Value;
                int price           = (int)nudOrderPrice.Value;
                string tradingType  = cboTradeType.Text;
                string oriOrderNo   = txtOriOrderNo.Text;

                int result = axKHOpenAPI1.SendOrder("주식주문", getScreenNo(), cboAccount.Text, OrderTypeCode(order_type), itemCode, quantity, price, TradeTypeCode(tradingType), oriOrderNo);

                if (result == 0)
                {
                    //성공
                    Log(string.Format("주문 요청 성공 : {0}", result), "[API]");
                }
                else
                {
                    //실패
                    Log(string.Format("주문 요청 실패 : {0}", result), "[API]");
                }
            }
            else if (sender.Equals(btn3))
            {
                /*
                 * SetRealReg() 파라메터
                 * 최대 가능 종목 수 = SetRealReg 1개 = FID
                 * FID 100개 까지, FID당 종목 100개 까지 = 최대 10,000 개 까지 설정 가능.
                 * 
                 * 1. 스크린넘버 1001 (자동 생성 스크린넘버는 1100-9999 범위에서 사용, API 규격은 0001-9999 까지)
                 * 2. jm_list 에 종목 목록이 들어간다. 다건인 경우 구분자; 포함 (최대 100개)
                 *      예) 009630;009631;...
                 * 3. 9001 종목코드,업종코드
                 *    10 현재가
                 *    11 전일대비
                 *    12 등락율
                 * 4. 0 기존목록(있다면) 해지 후 다시 등록 / 1 기존목록 + 추가 등록 시
                 * 
                 */

                string jm_list = "";

                foreach(DataRow r in dtAccount.Rows)
                {
                    if ("".Equals(jm_list))
                        jm_list += r["C8"].ToString().Replace("A", "");
                    else
                    {
                        jm_list += ";";
                        jm_list += r["C8"].ToString().Replace("A", "");
                    }
                }

                int result = axKHOpenAPI1.SetRealReg("1001", jm_list, "9001;10;11;12", "0");

            }
        }

        // ----------------------------------------------------------------------------------------------------
        // opw00018 : 계좌평가잔고내역요청 1: 합산, 2: 개별
        // 콜백 : AxKHOpenAPI1_OnReceiveTrData
        // ----------------------------------------------------------------------------------------------------
        private void getAccountEvaluate(string code)
        {
            if ( "1;2".IndexOf(code) == -1 )
            {
                Log(string.Format("계좌평가잔고내역요청 정의되지 않은 코드 {0}", code), "[API]");
                return;
            }

            // opw00018 : 계좌평가잔고내역요청
            Log(string.Format("계좌평가잔고내역요청 : {0}", code), "[API]");

            //계좌번호 = 전문 조회할 보유계좌번호
            axKHOpenAPI1.SetInputValue("계좌번호", cboAccount.Text);

            //비밀번호 = 사용안함(공백)
            axKHOpenAPI1.SetInputValue("비밀번호", "");

            //비밀번호입력매체구분 = 00
            axKHOpenAPI1.SetInputValue("비밀번호입력매체구분", "00");

            //조회구분 = 1:합산, 2:개별
            axKHOpenAPI1.SetInputValue("조회구분", code);

            // 전문을 서버로 보냄
            int result = axKHOpenAPI1.CommRqData(string.Format("계좌평가잔고내역요청{0}", code), "opw00018", 0, getScreenNo());

            // 전문 전송결과
            if (result == 0)
            {
                // 종목기본정보요청 성공
                Log(string.Format("계좌평가잔고내역요청 성공 : {0}", code), "[API]");
            }
            else
            {
                // 요청실패
                Log(string.Format("계좌평가잔고내역요청 실패 : {0}", code), "[API]");
            }
        }

        // ----------------------------------------------------------------------------------------------------
        // 로그 처리
        // ----------------------------------------------------------------------------------------------------
        private void Log(string message, string type)
        {
            string now_time = DateTime.Now.ToString("HH:mm:ss");
            txtLog.Text = string.Format("{0} {1} {2}", now_time, type, message + Environment.NewLine + txtLog.Text);
        }

        private void HaebiMain_Load(object sender, EventArgs e)
        {

        }

        private void initAccountOverall()
        {
            dtOverall.Columns.Add("C1"); // 총매입/총평가
            dtOverall.Columns.Add("C2"); // 총매입/총평가
            dtOverall.Columns.Add("C3"); // 평가손익/수익률(%)
            dtOverall.Columns.Add("C4"); // 평가손익/수익률(%)
            dtOverall.Columns.Add("C5"); // 매수가능/추정자산
            dtOverall.Columns.Add("C6"); // 매수가능/추정자산

            dtOverall.Rows.Add(new string[] { "총매입", "0", "평가손익", "0", "매수가능액", "0" });
            dtOverall.Rows.Add(new string[] { "총평가", "0", "수익률(%)", "0", "추정자산", "0" });

            dgvAccountOverall.DataSource = dtOverall;
            dgvAccountOverall.ColumnHeadersVisible = false;
            dgvAccountOverall.RowHeadersVisible = false;
            dgvAccountOverall.AllowUserToAddRows = false;
            dgvAccountOverall.ReadOnly = true;
            dgvAccountOverall.Enabled = false;
            dgvAccountOverall.ClearSelection();
        }

        private void initAccount()
        {
            dtAccount.Columns.Add("C1"); // 종목명
            dtAccount.Columns.Add("C2", typeof(long)); // 매입총액
            dtAccount.Columns.Add("C3", typeof(long)); // 평가손익
            dtAccount.Columns.Add("C4", typeof(double)); // 수익률
            dtAccount.Columns.Add("C5", typeof(long)); // 평균단가
            dtAccount.Columns.Add("C6", typeof(long)); // 현재가
            dtAccount.Columns.Add("C7", typeof(long)); // 보유수량
            dtAccount.Columns.Add("C8"); // 종목코드

            dgvAccount.DataSource = dtAccount;
            dgvAccount.Columns[0].HeaderText = "종목명";
            dgvAccount.Columns[1].HeaderText = "매입총액";
            dgvAccount.Columns[2].HeaderText = "평가손익";
            dgvAccount.Columns[3].HeaderText = "수익률";
            dgvAccount.Columns[4].HeaderText = "평균단가";
            dgvAccount.Columns[5].HeaderText = "현재가";
            dgvAccount.Columns[6].HeaderText = "보유수량";
            dgvAccount.Columns[7].HeaderText = "종목코드";

            // 소수점 2번째 자리 강제 표시
            dgvAccount.Columns[3].DefaultCellStyle.Format = "0.00";

            dgvAccount.RowHeadersVisible = false;
            dgvAccount.AllowUserToAddRows = false;
            dgvAccount.ReadOnly = true;
            dgvAccount.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvAccount.ClearSelection();
        }

        private void initOrderType()
        {
            cboOrderType.Items.Clear();

            cboOrderType.Items.AddRange(new object[] {
            "신규매수",
            "신규매도",
            "매수취소",
            "매도취소",
            "매수정정",
            "매도정정"});

            cboOrderType.SelectedIndex = 0;
        }

        private void initTradeType()
        {
            cboTradeType.Items.Clear();

            cboTradeType.Items.AddRange(new object[] {
            "지정가",
            "시장가",
            "조건부지정가",
            "최유리지정가",
            "최우선지정가",
            "지정가IOC",
            "시장가IOC",
            "최유리IOC",
            "지정가FOK",
            "시장가FOK",
            "최유리FOK",
            "장전시간외종가",
            "시간외단일가매매",
            "장후시간외종가"});

            cboTradeType.SelectedIndex = 0;
        }

        private string zTrimInt(string strInt)
        {
            return Convert.ToInt64(strInt).ToString();
        }

        private string zTrimFloat(string strFloat)
        {
            return float.Parse(strFloat).ToString("0.00", System.Globalization.CultureInfo.InvariantCulture);
        }

        private string strMinus(string strInt1, string strInt2)
        {
            return (Convert.ToInt64(strInt1) - Convert.ToInt64(strInt2)).ToString();
        }

        private string strDevide(string strInt1, string strInt2)
        {
            return (Convert.ToInt64(strInt1) / Convert.ToInt64(strInt2)).ToString();
        }

        private string getScreenNo()
        {
            if (screenNo >= 9999)
                screenNo = 1100;

            screenNo++;

            return screenNo.ToString();
        }

        private int OrderTypeCode(string order_type)
        {
            switch (order_type)
            {
                case "신규매수":
                    return 1;
                case "신규매도":
                    return 2;
                case "매수취소":
                    return 3;
                case "매도취소":
                    return 4;
                case "매수정정":
                    return 5;
                case "매도정정":
                    return 6;
                default:
                    return -1;
            }
        }

        private string TradeTypeCode(string trading_type)
        {
            switch (trading_type)
            {
                case "지정가":
                    return "00";
                case "시장가":
                    return "03";
                case "조건부지정가":
                    return "05";
                default:
                    return "";
            }
        }

        private void updatePrice(DataRow row, long price)
        {
            //      0|       1|       2|     3|       4|     5|       6|      7
            // 종목명|매입총액|평가손익|수익률|평균단가|현재가|보유수량|종목코드

            // 현재가
            row["C6"] = price;
            long price_now = (long)row["C6"];

            // 평균단가
            long price_avg = (long)row["C5"];

            // 수량
            long qty = (long)row["C7"];

            // 평가손익 = (현재가 - 평균단가) x 보유수량
            long eval = (price_now - price_avg) * qty;
            row["C3"] = eval;

            // 수익률 = ((현재가 / 평균단가) - 1) * 100
            double profit_ratio = (((double)price_now / (double)price_avg) - 1) * 100;
            profit_ratio = Math.Round(profit_ratio, 2);
            row["C4"] = profit_ratio;
        }
    }
}
