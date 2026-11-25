
<%@ Page Title="Kết quả thi" Language="C#"
    MasterPageFile="~/Site.Master"
    AutoEventWireup="true"
    CodeBehind="KetQuaThi.aspx.cs"
    Inherits="WebsiteThiTracNghiem2.KetQuaThi" %>

<asp:Content ID="TitleContent1" ContentPlaceHolderID="TitleContent" runat="server">
    Kết quả thi
</asp:Content>

<asp:Content ID="HeadContent1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="css/KetQuaThi.css" rel="stylesheet" />
</asp:Content>

<asp:Content ID="MainContent1" ContentPlaceHolderID="MainContent" runat="server">

    <div class="result-container">

        <div class="result-header">
            <span>QUIZ ONLINE</span>
            <h1>Kết quả thi</h1>
            <p>Theo dõi và xem lại kết quả các bài thi của bạn.</p>
        </div>

        <div class="result-content">

            <div class="result-title">
                <h2>Lịch sử kết quả</h2>
                <asp:Label ID="lblSoKetQua" runat="server" />
            </div>

            <asp:Label ID="lblThongBao" runat="server"
                CssClass="result-message" />

            <div class="result-table-wrapper">

                <asp:Repeater ID="rptKetQua" runat="server">
                    <HeaderTemplate>
                        <table class="result-table">
                            <thead>
                                <tr>
                                    <th>STT</th>
                                    <th>Đề thi</th>
                                    <th>Số câu đúng</th>
                                    <th>Tổng câu</th>
                                    <th>Điểm</th>
                                    <th>Ngày thi</th>
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
                                <span class="score">
                                    <%#: Eval("Diem") %>
                                </span>
                            </td>
                            <td><%#: Eval("NgayThi") %></td>
                        </tr>
                    </ItemTemplate>

                    <FooterTemplate>
                            </tbody>
                        </table>
                    </FooterTemplate>

                </asp:Repeater>

            </div>

            <asp:HyperLink ID="lnkDanhSach" runat="server"
                NavigateUrl="~/DanhSachDeThi.aspx"
                CssClass="btn-back">
                ← Quay lại danh sách đề thi
            </asp:HyperLink>

        </div>

    </div>

</asp:Content>

