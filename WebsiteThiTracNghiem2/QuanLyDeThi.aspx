
<%@ Page Title="Quản lý đề thi" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="QuanLyDeThi.aspx.cs" Inherits="WebsiteThiTracNghiem2.QuanLyDeThi" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Quản lý đề thi
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="HeadContent" runat="server">
    <style>
        .ql-container {
            max-width: 1100px;
            margin: 30px auto;
            padding: 25px;
            background: #fff;
            border-radius: 12px;
            box-shadow: 0 3px 15px rgba(0,0,0,.08);
        }

        .ql-container h2 {
            color: #244a9b;
            margin-bottom: 20px;
        }

        .form-grid {
            display: grid;
            grid-template-columns: repeat(2, 1fr);
            gap: 15px;
            margin-bottom: 20px;
        }

        .form-grid label {
            display: block;
            font-weight: 600;
            margin-bottom: 6px;
        }

        .form-grid input {
            width: 100%;
            padding: 10px;
            border: 1px solid #ccc;
            border-radius: 6px;
            box-sizing: border-box;
        }

        .btn {
            padding: 9px 16px;
            border: none;
            border-radius: 6px;
            color: white;
            cursor: pointer;
            margin: 3px;
        }

        .btn-add { background: #198754; }
        .btn-update { background: #0d6efd; }
        .btn-cancel { background: #6c757d; }
        .btn-edit { background: #0d6efd; }
        .btn-delete { background: #dc3545; }

        .message {
            display: block;
            margin: 15px 0;
            font-weight: bold;
        }

        .table-wrap {
            overflow-x: auto;
        }

        .exam-table {
            width: 100%;
            border-collapse: collapse;
            margin-top: 20px;
        }

        .exam-table th, .exam-table td {
            border: 1px solid #ddd;
            padding: 11px;
            text-align: center;
        }

        .exam-table th {
            background: #244a9b;
            color: white;
        }

        @media(max-width: 650px) {
            .form-grid { grid-template-columns: 1fr; }
        }
    </style>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <div class="ql-container">
        <h2>QUẢN LÝ ĐỀ THI</h2>

        <asp:Label ID="lblThongBao" runat="server" CssClass="message"></asp:Label>

        <h3>Thông tin đề thi</h3>

        <div class="form-grid">
            <div>
                <label>Mã đề thi</label>
                <asp:TextBox ID="txtMaDe" runat="server" />
            </div>

            <div>
                <label>Tên đề thi</label>
                <asp:TextBox ID="txtTenDe" runat="server" />
            </div>

            <div>
                <label>Môn học</label>
                <asp:TextBox ID="txtMonHoc" runat="server" />
            </div>

            <div>
                <label>Thời gian (phút)</label>
                <asp:TextBox ID="txtThoiGian" runat="server" TextMode="Number" />
            </div>

            <div>
                <label>Số câu hỏi</label>
                <asp:TextBox ID="txtSoCau" runat="server" TextMode="Number" />
            </div>
        </div>

        <asp:Button ID="btnThem" runat="server" Text="Thêm đề thi"
            CssClass="btn btn-add" OnClick="btnThem_Click" />

        <asp:Button ID="btnCapNhat" runat="server" Text="Cập nhật"
            CssClass="btn btn-update" OnClick="btnCapNhat_Click" />

        <asp:Button ID="btnLamMoi" runat="server" Text="Làm mới"
            CssClass="btn btn-cancel" OnClick="btnLamMoi_Click" />

        <hr />

        <h3>Danh sách đề thi</h3>

        <div class="table-wrap">
            <asp:GridView ID="gvDeThi" runat="server"
                AutoGenerateColumns="False"
                CssClass="exam-table"
                DataKeyNames="MaDe"
                OnRowCommand="gvDeThi_RowCommand"
                EmptyDataText="Chưa có đề thi nào.">

                <Columns>
                    <asp:BoundField DataField="MaDe" HeaderText="Mã đề" />
                    <asp:BoundField DataField="TenDe" HeaderText="Tên đề" />
                    <asp:BoundField DataField="MonHoc" HeaderText="Môn học" />
                    <asp:BoundField DataField="ThoiGian" HeaderText="Thời gian (phút)" />
                    <asp:BoundField DataField="SoCau" HeaderText="Số câu" />

                    <asp:TemplateField HeaderText="Thao tác">
                        <ItemTemplate>
                            <asp:Button ID="btnSua" runat="server"
                                Text="Sửa" CssClass="btn btn-edit"
                                CommandName="SuaDe"
                                CommandArgument='<%# Eval("MaDe") %>' />

                            <asp:Button ID="btnXoa" runat="server"
                                Text="Xóa" CssClass="btn btn-delete"
                                CommandName="XoaDe"
                                CommandArgument='<%# Eval("MaDe") %>'
                                OnClientClick="return confirm('Bạn có chắc muốn xóa đề thi này?');" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
    </div>
</asp:Content>
