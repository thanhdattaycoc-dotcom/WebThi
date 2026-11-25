
<%@ Page Title="Làm bài thi" Language="C#"
    MasterPageFile="~/Site.Master"
    AutoEventWireup="true"
    CodeBehind="LamBaiThi.aspx.cs"
    Inherits="WebsiteThiTracNghiem2.LamBaiThi" %>

<asp:Content ID="TitleContent1" ContentPlaceHolderID="TitleContent" runat="server">
    Làm bài thi
</asp:Content>

<asp:Content ID="HeadContent1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="css/LamBaiThi.css" rel="stylesheet" />
</asp:Content>

<asp:Content ID="MainContent1" ContentPlaceHolderID="MainContent" runat="server">

    <div class="test-container">

        <div class="test-header">
            <span class="test-label">QUIZ ONLINE</span>
            <h1><asp:Label ID="lblTenDe" runat="server" /></h1>

            <div class="test-information">
                <span>Môn học: <asp:Label ID="lblMonHoc" runat="server" /></span>
                <span>Thời gian: <asp:Label ID="lblThoiGian" runat="server" /> phút</span>
                <span>Số câu: <asp:Label ID="lblSoCau" runat="server" /></span>
            </div>
        </div>

        <asp:Label ID="lblThongBao" runat="server"
            CssClass="test-message" />

        <asp:Panel ID="pnlBaiThi" runat="server">

            <div class="test-instruction">
                Chọn một đáp án đúng cho mỗi câu hỏi.
            </div>

            <asp:Repeater ID="rptCauHoi" runat="server">
                <ItemTemplate>

                    <div class="question-card">

                        <h3>
                            Câu <%# Container.ItemIndex + 1 %>:
                            <%#: Eval("NoiDung") %>
                        </h3>

                        <div class="answer-list">

                            <label>
                                <input type="radio"
                                    name='<%# "cau_" + Eval("MaCauHoi") %>'
                                    value="A" />
                                A. <%#: Eval("DapAnA") %>
                            </label>

                            <label>
                                <input type="radio"
                                    name='<%# "cau_" + Eval("MaCauHoi") %>'
                                    value="B" />
                                B. <%#: Eval("DapAnB") %>
                            </label>

                            <label>
                                <input type="radio"
                                    name='<%# "cau_" + Eval("MaCauHoi") %>'
                                    value="C" />
                                C. <%#: Eval("DapAnC") %>
                            </label>

                            <label>
                                <input type="radio"
                                    name='<%# "cau_" + Eval("MaCauHoi") %>'
                                    value="D" />
                                D. <%#: Eval("DapAnD") %>
                            </label>

                        </div>

                    </div>

                </ItemTemplate>
            </asp:Repeater>

            <asp:Button ID="btnNopBai" runat="server"
                Text="Nộp bài thi"
                CssClass="btn-submit"
                OnClick="btnNopBai_Click"
                OnClientClick="return confirm('Bạn có chắc chắn muốn nộp bài?');" />

        </asp:Panel>

        <asp:Panel ID="pnlKetQua" runat="server" Visible="false">

            <div class="result-card">
                <h2>Kết quả bài thi</h2>

                <p>Số câu đúng:
                    <asp:Label ID="lblSoCauDung" runat="server" />
                </p>

                <p>Tổng số câu:
                    <asp:Label ID="lblTongSoCau" runat="server" />
                </p>

                <div class="result-score">
                    <asp:Label ID="lblDiem" runat="server" />
                </div>

                <asp:HyperLink ID="lnkVeDanhSach" runat="server"
                    NavigateUrl="~/DanhSachDeThi.aspx"
                    CssClass="btn-back">
                    Quay lại danh sách đề thi
                </asp:HyperLink>
            </div>

        </asp:Panel>

    </div>

</asp:Content>
