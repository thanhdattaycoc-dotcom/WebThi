
<%@ Page Title="Danh sách đề thi" Language="C#"
    MasterPageFile="~/Site.Master"
    AutoEventWireup="true"
    CodeBehind="DanhSachDeThi.aspx.cs"
    Inherits="WebsiteThiTracNghiem2.DanhSachDeThi" %>

<asp:Content ID="TitleContent1" ContentPlaceHolderID="TitleContent" runat="server">
    Danh sách đề thi
</asp:Content>

<asp:Content ID="HeadContent1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="CSS/DanhSachDeThi.css" rel="stylesheet" />
    <script src="JS/DanhSachDeThi.js" defer></script>
</asp:Content>

<asp:Content ID="MainContent1" ContentPlaceHolderID="MainContent" runat="server">

    <div class="exam-container">

        <div class="exam-header">
            <span class="exam-label">QUIZ ONLINE</span>
            <h1>Khám phá đề thi</h1>
            <p>Lựa chọn đề thi phù hợp và kiểm tra kiến thức của bạn.</p>
        </div>

        <div class="exam-search">
            <input type="text"
                   id="txtTimKiemDeThi"
                   placeholder="Tìm kiếm theo tên đề thi hoặc môn học..." />

            <span class="search-icon">⌕</span>
        </div>

        <div class="exam-count">
            <h2>Danh sách đề thi</h2>
            <asp:Label ID="lblSoDeThi" runat="server" Text="0 đề thi"></asp:Label>
        </div>

        <asp:Label ID="lblThongBao" runat="server"
            CssClass="exam-message"></asp:Label>

        <div class="exam-grid" id="danhSachDeThi">

            <asp:Repeater ID="rptDeThi" runat="server">
                <ItemTemplate>

                    <div class="exam-card">

                        <div class="exam-card-top">
                            <span class="exam-code">
                                <%# Eval("MaDe") %>
                            </span>
                            <span class="exam-icon">📝</span>
                        </div>

                        <h3 class="exam-name">
                            <%# Eval("TenDe") %>
                        </h3>

                        <p class="exam-subject">
                            Môn học: <%# Eval("MonHoc") %>
                        </p>

                        <div class="exam-info">
                            <div>
                                <span>⏱</span>
                                <%# Eval("ThoiGian") %> phút
                            </div>

                            <div>
                                <span>☰</span>
                                <%# Eval("SoCau") %> câu hỏi
                            </div>
                        </div>

                        <asp:HyperLink ID="lnkBatDau" runat="server"
                            CssClass="btn-start"
                            NavigateUrl='<%# "LamBaiThi.aspx?MaDe=" + Eval("MaDe") %>'>
                            Bắt đầu thi →
                        </asp:HyperLink>

                    </div>

                </ItemTemplate>
            </asp:Repeater>

        </div>

        <div id="khongTimThay" class="no-result" style="display:none;">
            Không tìm thấy đề thi phù hợp.
        </div>

    </div>

</asp:Content>
