
<%@ Page Title="Trang chủ" Language="C#" MasterPageFile="~/Site.Master"
    AutoEventWireup="true" CodeBehind="TrangChu.aspx.cs"
    Inherits="WebsiteThiTracNghiem2.TrangChu" %>

<asp:Content ID="ContentTitle" ContentPlaceHolderID="TitleContent" runat="server">
    Trang chủ
</asp:Content>

<asp:Content ID="ContentHead" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="<%= ResolveUrl("~/CSS/TrangChu.css") %>" rel="stylesheet" />
</asp:Content>

<asp:Content ID="ContentMain" ContentPlaceHolderID="MainContent" runat="server">

    <div class="trang-chu">

        <!-- Banner giới thiệu -->
        <section class="banner">
            <div class="banner-content">
                <span class="banner-label">QUIZ ONLINE</span>

                <h1>Chinh phục kiến thức<br />
                    <span>Qua từng bài trắc nghiệm</span>
                </h1>

                <p>
                    Hệ thống thi trắc nghiệm trực tuyến giúp bạn
                    ôn tập, kiểm tra và đánh giá kiến thức một cách hiệu quả.
                </p>

                <asp:Label ID="lblLoiChao" runat="server"
                    CssClass="loi-chao"></asp:Label>

                <div class="banner-buttons">
                    <asp:HyperLink ID="lnkBatDauThi" runat="server"
                        NavigateUrl="~/DanhSachDeThi.aspx"
                        CssClass="btn-primary">
                        Bắt đầu thi
                    </asp:HyperLink>

                    <a href="#gioi-thieu" class="btn-secondary">
                        Tìm hiểu thêm
                    </a>
                </div>
            </div>

            <div class="banner-icon">
                <div class="icon-circle">
                    <span>📝</span>
                </div>
                <div class="icon-small icon-one">✓</div>
                <div class="icon-small icon-two">A+</div>
                <div class="icon-small icon-three">★</div>
            </div>
        </section>

        <!-- Giới thiệu chức năng -->
        <section class="section-chuc-nang" id="gioi-thieu">

            <div class="section-heading">
                <span>KHÁM PHÁ HỆ THỐNG</span>
                <h2>Các chức năng nổi bật</h2>
                <p>Những công cụ hỗ trợ học tập và kiểm tra kiến thức.</p>
            </div>

            <div class="chuc-nang-grid">

                <div class="chuc-nang-card">
                    <div class="card-icon">📚</div>
                    <h3>Danh sách đề thi</h3>
                    <p>Xem và lựa chọn các đề thi trắc nghiệm phù hợp.</p>
                    <a href="<%= ResolveUrl("~/DanhSachDeThi.aspx") %>">
                        Xem đề thi →
                    </a>
                </div>

                <div class="chuc-nang-card">
                    <div class="card-icon">✍️</div>
                    <h3>Làm bài thi</h3>
                    <p>Tham gia làm bài và kiểm tra kiến thức trực tuyến.</p>
                    <a href="<%= ResolveUrl("~/DanhSachDeThi.aspx") %>">
                        Bắt đầu →
                    </a>
                </div>

                <div class="chuc-nang-card">
                    <div class="card-icon">🏆</div>
                    <h3>Kết quả thi</h3>
                    <p>Theo dõi điểm số và kết quả sau mỗi bài thi.</p>
                    <a href="<%= ResolveUrl("~/LichSuThi.aspx") %>">
                        Xem lịch sử →
                    </a>
                </div>

            </div>
        </section>

        <!-- Hướng dẫn -->
        <section class="huong-dan">
            <div class="section-heading">
                <span>HƯỚNG DẪN</span>
                <h2>Tham gia thi chỉ với 3 bước</h2>
            </div>

            <div class="buoc-grid">
                <div class="buoc-item">
                    <div class="buoc-so">01</div>
                    <h3>Đăng nhập</h3>
                    <p>Đăng nhập tài khoản để sử dụng hệ thống.</p>
                </div>

                <div class="buoc-item">
                    <div class="buoc-so">02</div>
                    <h3>Chọn đề thi</h3>
                    <p>Lựa chọn môn học và đề thi muốn tham gia.</p>
                </div>

                <div class="buoc-item">
                    <div class="buoc-so">03</div>
                    <h3>Hoàn thành bài thi</h3>
                    <p>Nộp bài và xem kết quả thi của bạn.</p>
                </div>
            </div>
        </section>

        <!-- Lời kết -->
        <section class="loi-moi">
            <h2>Sẵn sàng kiểm tra kiến thức của bạn?</h2>
            <p>Bắt đầu hành trình học tập và chinh phục những thử thách mới.</p>

            <asp:HyperLink ID="lnkThiNgay" runat="server"
                NavigateUrl="~/DanhSachDeThi.aspx"
                CssClass="btn-primary">
                Khám phá đề thi ngay →
            </asp:HyperLink>
        </section>

    </div>

</asp:Content>
