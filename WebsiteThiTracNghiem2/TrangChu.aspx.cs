
using System;

namespace WebsiteThiTracNghiem2
{
    public partial class TrangChu : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                HienThiLoiChao();
            }
        }

        // Hiển thị lời chào theo trạng thái đăng nhập
        private void HienThiLoiChao()
        {
            if (Session["TenDangNhap"] != null)
            {
                string tenDangNhap = Session["TenDangNhap"].ToString();

                lblLoiChao.Text = "Xin chào, " +
                    Server.HtmlEncode(tenDangNhap) + "!";
            }
            else
            {
                lblLoiChao.Text = "Chào mừng bạn đến với QUIZ ONLINE!";
            }
        }
    }
}
