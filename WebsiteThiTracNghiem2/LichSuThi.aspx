
<%@ Page Title="Lịch sử thi" Language="C#"
    MasterPageFile="~/Site.Master"
    AutoEventWireup="true"
    CodeBehind="LichSuThi.aspx.cs"
    Inherits="WebsiteThiTracNghiem2.LichSuThi" %>

<asp:Content ID="TitleContent1" ContentPlaceHolderID="TitleContent" runat="server">
    Lịch sử thi
</asp:Content>

<asp:Content ID="HeadContent1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="css/LichSuThi.css" rel="stylesheet" />
</asp:Content>

<asp:Content ID="MainContent1" ContentPlaceHolderID="MainContent" runat="server">

    <div class="history-container">

        <div class="history-header">
            <span class="history-label">QUIZ ONLINE</span>
            <h1>Lịch sử thi</h1>
            <p>Theo dõi quá trình học tập và kết quả các lần thi của bạn.</p>
        </div>

        <div class="history-summary">

            <div class="summary-card">
                <span class="summary-icon">📝</span>
                <div>
                    <p>Tổng số lần thi</p>
                    <h2>
                        <asp:Label ID="lblTongLanThi" runat="server" Text="0" />
                    </h2>
                </div>
            </div>

            <div class="summary-card">
                <span class="summary-icon">⭐</span>
                <div>
                    <p>Điểm trung bình</p>
                    <h2>
                        <asp:Label ID="lblDiemTrungBinh" runat="server" Text="0.00" />
                    </h2>
                </div>
            </div>

            <div class="summary-card">
                <span class="summary-icon">🏆</span>
                <div>
                    <p>Điểm cao nhất</p>
                    <h2>
                        <asp:Label ID="lblDiemCaoNhat" runat="server" Text="0.00" />
                    </h2>
                </div>
            </div>

        </div>

        <div class="history-content">

            <div class="history-title">
                <div>
                    <h2>Các bài thi đã thực hiện</h2>
                    <p>Danh sách kết quả thi của tài khoản hiện tại.</p>
                </div>

                <asp:Label ID="lblSoKetQua" runat="server"
                    CssClass="history-count" />
            </div>

            <asp:Label ID="lblThongBao" runat="server"
                CssClass="history-message" />

            <div class="history-table-wrapper">

                <asp:Repeater ID="rptLichSu" runat="server">

                    <HeaderTemplate>
                        <table class="history-table">
                            <thead>
                                <tr>
                                    <th>STT</th>
                                    <th>Tên đề thi</th>
                                    <th>Số câu đúng</th>
                                    <th>Tổng câu</th>
                                    <th>Điểm</th>
                                    <th>Ngày thi</th>
                                    <th>Kết quả</th>
                                </tr>
                            </thead>
                            <tbody>
                    </HeaderTemplate>

                    <ItemTemplate>
                        <tr>
                            <td><%# Container.ItemIndex + 1 %></td>

                            <td>
                                <strong><%#: Eval("TenDe") %></strong>
                                <small><%#: Eval("MaDe") %></small>
                            </td>

                            <td><%#: Eval("SoCauDung") %></td>

                            <td><%#: Eval("TongSoCau") %></td>

                            <td>
                                <span class="history-score">
                                    <%#: Eval("Diem") %>
                                </span>
                            </td>

                            <td><%#: Eval("NgayThi") %></td>

                            <td>
                                <span class='<%# Convert.ToDouble(Eval("Diem")) >= 5 ? "status-pass" : "status-fail" %>'>
                                    <%# Convert.ToDouble(Eval("Diem")) >= 5 ? "Đạt" : "Chưa đạt" %>
                                </span>
                            </td>
                        </tr>
                    </ItemTemplate>

                    <FooterTemplate>
                            </tbody>
                        </table>
                    </FooterTemplate>

                </asp:Repeater>

            </div>

            <asp:HyperLink ID="lnkQuayLai" runat="server"
                NavigateUrl="~/TrangChu.aspx"
                CssClass="btn-home">
                ← Quay lại trang chủ
            </asp:HyperLink>

        </div>

    </div>

</asp:Content>
