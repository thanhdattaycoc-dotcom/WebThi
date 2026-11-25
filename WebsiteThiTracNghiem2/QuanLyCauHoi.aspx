
<%@ Page Title="Quản lý câu hỏi" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="QuanLyCauHoi.aspx.cs" Inherits="WebsiteThiTracNghiem2.QuanLyCauHoi" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Quản lý câu hỏi
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="HeadContent" runat="server">
    <style>
        .question-container {
            max-width: 1100px;
            margin: 30px auto;
            padding: 25px;
            background: white;
            border-radius: 12px;
            box-shadow: 0 3px 15px rgba(0,0,0,.08);
        }

        .question-container h2 { color: #244a9b; }

        .question-form {
            display: grid;
            grid-template-columns: 1fr 1fr;
            gap: 15px;
        }

        .question-form label {
            display: block;
            font-weight: bold;
            margin-bottom: 6px;
        }

        .question-form input,
        .question-form select,
        .question-form textarea {
            width: 100%;
            padding: 10px;
            border: 1px solid #ccc;
            border-radius: 6px;
            box-sizing: border-box;
        }

        .full-width { grid-column: 1 / -1; }

        .q-button {
            padding: 9px 15px;
            margin: 4px;
            border: none;
            border-radius: 6px;
            color: white;
            cursor: pointer;
        }

        .add { background: #198754; }
        .update { background: #0d6efd; }
        .reset { background: #6c757d; }
        .edit { background: #0d6efd; }
        .delete { background: #dc3545; }

        .message {
            display: block;
            margin: 15px 0;
            font-weight: bold;
        }

        .question-table {
            width: 100%;
            border-collapse: collapse;
            margin-top: 20px;
        }

        .question-table th,
        .question-table td {
            border: 1px solid #ddd;
            padding: 10px;
            text-align: left;
        }

        .question-table th {
            background: #244a9b;
            color: white;
        }

        .table-wrap { overflow-x: auto; }

        @media(max-width: 650px) {
            .question-form { grid-template-columns: 1fr; }
            .full-width { grid-column: auto; }
        }
    </style>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <div class="question-container">
        <h2>QUẢN LÝ CÂU HỎI</h2>

        <asp:Label ID="lblThongBao" runat="server" CssClass="message"></asp:Label>

        <div class="question-form">
            <div>
                <label>Mã câu hỏi</label>
                <asp:TextBox ID="txtMaCauHoi" runat="server" />
            </div>

            <div>
                <label>Chọn đề thi</label>
                <asp:DropDownList ID="ddlMaDe" runat="server" />
            </div>

            <div class="full-width">
                <label>Nội dung câu hỏi</label>
                <asp:TextBox ID="txtNoiDung" runat="server"
                    TextMode="MultiLine" Rows="3" />
            </div>

            <div>
                <label>Đáp án A</label>
                <asp:TextBox ID="txtA" runat="server" />
            </div>

            <div>
                <label>Đáp án B</label>
                <asp:TextBox ID="txtB" runat="server" />
            </div>

            <div>
                <label>Đáp án C</label>
                <asp:TextBox ID="txtC" runat="server" />
            </div>

            <div>
                <label>Đáp án D</label>
                <asp:TextBox ID="txtD" runat="server" />
            </div>

            <div>
                <label>Đáp án đúng</label>
                <asp:DropDownList ID="ddlDapAnDung" runat="server">
                    <asp:ListItem Value="">-- Chọn đáp án --</asp:ListItem>
                    <asp:ListItem Value="A">A</asp:ListItem>
                    <asp:ListItem Value="B">B</asp:ListItem>
                    <asp:ListItem Value="C">C</asp:ListItem>
                    <asp:ListItem Value="D">D</asp:ListItem>
                </asp:DropDownList>
            </div>
        </div>

        <br />

        <asp:Button ID="btnThem" runat="server" Text="Thêm câu hỏi"
            CssClass="q-button add" OnClick="btnThem_Click" />

        <asp:Button ID="btnCapNhat" runat="server" Text="Cập nhật"
            CssClass="q-button update" OnClick="btnCapNhat_Click" />

        <asp:Button ID="btnLamMoi" runat="server" Text="Làm mới"
            CssClass="q-button reset" OnClick="btnLamMoi_Click" />

        <hr />

        <h3>Danh sách câu hỏi</h3>

        <div class="table-wrap">
            <asp:GridView ID="gvCauHoi" runat="server"
                AutoGenerateColumns="False"
                CssClass="question-table"
                DataKeyNames="MaCauHoi"
                OnRowCommand="gvCauHoi_RowCommand"
                EmptyDataText="Chưa có câu hỏi nào.">

                <Columns>
                    <asp:BoundField DataField="MaCauHoi" HeaderText="Mã câu hỏi" />
                    <asp:BoundField DataField="MaDe" HeaderText="Mã đề" />
                    <asp:BoundField DataField="NoiDung" HeaderText="Nội dung" />
                    <asp:BoundField DataField="DapAnDung" HeaderText="Đáp án đúng" />

                    <asp:TemplateField HeaderText="Thao tác">
                        <ItemTemplate>
                            <asp:Button ID="btnSua" runat="server"
                                Text="Sửa" CssClass="q-button edit"
                                CommandName="SuaCauHoi"
                                CommandArgument='<%# Eval("MaCauHoi") %>' />

                            <asp:Button ID="btnXoa" runat="server"
                                Text="Xóa" CssClass="q-button delete"
                                CommandName="XoaCauHoi"
                                CommandArgument='<%# Eval("MaCauHoi") %>'
                                OnClientClick="return confirm('Bạn có chắc muốn xóa câu hỏi này?');" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
    </div>
</asp:Content>
