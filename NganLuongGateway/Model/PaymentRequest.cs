using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace NganLuongGateway.Model
{
    public class PaymentRequest
    {
        public string nganluong_url = "https://sandbox.nganluong.vn:8088/nl35/checkout.php";
        public string merchant_site_code = "48840";
        public string secure_pass = "0ac2592dc9c49a99253bebc6eb981a22";
        public PaymentRequest()
        {
        }

        public string CreateMD5Hash1(string input)
        {
            // Use input string to calculate MD5 hash
            MD5 md5 = System.Security.Cryptography.MD5.Create();
            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
            byte[] hashBytes = md5.ComputeHash(inputBytes);

            // Convert the byte array to hexadecimal string
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hashBytes.Length; i++)
            {
                sb.Append(hashBytes[i].ToString("x2"));
                // To force the hex string to lower-case letters instead of
                // upper-case, use he following line instead:
                // sb.Append(hashBytes[i].ToString("x2")); 
            }
            return sb.ToString();
        }

        public string GetMD5Hash(String input)
        {

            System.Security.Cryptography.MD5CryptoServiceProvider x = new System.Security.Cryptography.MD5CryptoServiceProvider();

            byte[] bs = System.Text.Encoding.UTF8.GetBytes(input);

            bs = x.ComputeHash(bs);

            System.Text.StringBuilder s = new System.Text.StringBuilder();

            foreach (byte b in bs)
            {

                s.Append(b.ToString("x2").ToLower());

            }

            String md5String = s.ToString();

            return md5String;
        }

        public string buildCheckoutUrlNew(NLModel nl)
        {
            //order_description = "Bình nước & Lock Lock 1.2 lít";
            //buyer_info = "Họ tên người mua *|* Địa chỉ Email *|* Điện thoại *|* Địa chỉ nhận hàng";// "Họ tên người mua *|* Địa chỉ Email *|* Điện thoại *|* Địa chỉ nhận hàng";
            string str_return_url = nl.return_url.ToLower();
            string security_code = "";
            security_code += "" + merchant_site_code;
            security_code += " " + str_return_url;
            security_code += " " + nl.receiver;//tài khoản ngân lượng
            security_code += " " + nl.transaction_info;
            security_code += " " + nl.order_code;
            security_code += " " + nl.price;
            security_code += " " + nl.currency;//hỗ trợ 2 loại tiền tệ currency usd,vnd
            security_code += " " + nl.quantity;//số lượng mặc định 1
            security_code += " " + nl.tax;
            security_code += " " + nl.discount;
            security_code += " " + nl.fee_cal;
            security_code += " " + nl.fee_shipping;
            security_code += " " + nl.order_description;
            string payinfo = "";// Convert.ToString("Họ tên người mua *|* Địa chỉ Email *|* Điện thoại *|* Địa chỉ nhận hàng");
            security_code += " " + nl.buyer_info;
            security_code += " " + nl.affiliate_code;
            security_code += " " + secure_pass;
            //return security_code;
            string md5 = GetMD5Hash(security_code);

            //security_code += " " + md5;
            Hashtable ht = new Hashtable();

            ht.Add("merchant_site_code", merchant_site_code);
            ht.Add("return_url", HttpUtility.UrlEncode(str_return_url).ToLower());
            ht.Add("receiver", HttpUtility.UrlEncode(nl.receiver));
            ht.Add("transaction_info", nl.transaction_info);
            ht.Add("order_code", nl.order_code);
            ht.Add("price", nl.price);
            ht.Add("currency", nl.currency);
            ht.Add("quantity", nl.quantity);
            ht.Add("tax", nl.tax);
            ht.Add("discount", nl.discount);
            ht.Add("fee_cal", nl.fee_cal);
            ht.Add("fee_shipping", nl.fee_shipping);
            ht.Add("order_description", HttpUtility.UrlEncode(nl.order_description));
            ht.Add("buyer_info", HttpUtility.UrlEncode(nl.buyer_info));// "Họ tên người mua *|* Địa chỉ Email *|* Điện thoại *|* Địa chỉ nhận hàng");// "Họ tên người mua *|* Địa chỉ Email *|* Điện thoại *|* Địa chỉ nhận hàng");
            ht.Add("affiliate_code", nl.affiliate_code);
            ht.Add("secure_code", md5);
            ht.Add("cancel_url", nl.cancel_url);
            ht.Add("time_limit", nl.time_limit);
            // T?o url redirect

            string redirect_url = this.nganluong_url;

            if (redirect_url.IndexOf("?") == -1)
            {
                redirect_url += "?";
            }
            else if (redirect_url.Substring(redirect_url.Length - 1, 1) != "?" && redirect_url.IndexOf("&") == -1)
            {
                redirect_url += "&";
            }

            string url = "";

            // Duy?t các ph?n t? trong m?ng bam ht1 d? t?o redirect url
            IDictionaryEnumerator en = ht.GetEnumerator();

            while (en.MoveNext())
            {
                if (url == "")
                    url += en.Key.ToString() + "=" + en.Value.ToString();
                else
                    url += "&" + en.Key.ToString() + "=" + en.Value.ToString();
            }

            string rdu = redirect_url + url;

            return rdu;
        }


        public Boolean verifyPaymentUrl(String transaction_info, String order_code, String price, String payment_id, String payment_type, String error_text, String secure_code)
        {
            String str = "";

            str += " " + HttpUtility.HtmlDecode(transaction_info);

            str += " " + order_code;

            str += " " + price;

            str += " " + payment_id;

            str += " " + payment_type;

            str += " " + HttpUtility.HtmlDecode(error_text);

            str += " " + this.merchant_site_code;

            str += " " + this.secure_pass;

            // Mã hóa các tham s?
            String verify_secure_code = "";

            verify_secure_code = this.GetMD5Hash(str);

            // Xác th?c mã c?a ch? web v?i mã tr? v? t? nganluong.vn
            if (verify_secure_code == secure_code) return true;

            return false;
        }


        public string base64Encode(string data)
        {
            try
            {
                byte[] encData_byte = new byte[data.Length];
                encData_byte = System.Text.Encoding.UTF8.GetBytes(data);
                string encodedData = Convert.ToBase64String(encData_byte);
                return encodedData;
            }
            catch (Exception e)
            {
                throw new Exception("Error in base64Encode" + e.Message);
            }
        }

        public string base64Decode(string data)
        {
            try
            {
                System.Text.UTF8Encoding encoder = new System.Text.UTF8Encoding();
                System.Text.Decoder utf8Decode = encoder.GetDecoder();

                byte[] todecode_byte = Convert.FromBase64String(data);
                int charCount = utf8Decode.GetCharCount(todecode_byte, 0, todecode_byte.Length);
                char[] decoded_char = new char[charCount];
                utf8Decode.GetChars(todecode_byte, 0, todecode_byte.Length, decoded_char, 0);
                string result = new String(decoded_char);
                return result;
            }
            catch (Exception e)
            {
                throw new Exception("Error in base64Decode" + e.Message);
            }
        }

        public string Base64Encode1(string source)
        {
            byte[] b = Encoding.ASCII.GetBytes(source);

            return Convert.ToBase64String(b);
        }


        //////// Xây dựng hàm gọi lại kiểm tra đơn hàng mới thanh toán

        //public string Nl_checkoder(int total, string order_code, string paymentID)
        //{

        //    string param = "";
        //    param = "<ORDERS><TOTAL>1</TOTAL><ORDER><ORDER_CODE>" + order_code + "</ORDER_CODE><PAYMENT_ID>" + paymentID + "</PAYMENT_ID></ORDER></ORDERS>";
        //    string paramdata = this.base64Encode(param);

        //    string checksum = this.GetMD5Hash(this.merchant_site_code + this.secure_pass);

        //    APInlcheckorder.NGANLUONG_API nlcheck = new APInlcheckorder.NGANLUONG_API();

        //    string result = nlcheck.checkOrder_v2(this.merchant_site_code, paramdata, checksum);

        //    //string result = nlcheck.checkOrder_v2("21971", "PE9SREVSUz48VE9UQUw+MTwvVE9UQUw+PE9SREVSPjxPUkRFUl9DT0RFPjAwODI2NzwvT1JERVJfQ09ERT48UEFZTUVOVF9JRD48L1BBWU1FTlRfSUQ+PC9PUkRFUj48L09SREVSUz4=", "4a1039aa996976ddfcbaf48db3a13d7d");

        //    return this.base64Decode(result);
        //}
    }
}