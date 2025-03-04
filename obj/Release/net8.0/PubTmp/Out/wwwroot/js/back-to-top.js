//导航置顶
$(function () {
    $(window).scroll(function (event) {
        var top = $(document).scrollTop();
        if (top > 49) {
            $(".header").addClass("nav-active");
            $('.banner').removeClass('nav-banner');
        
        } else {
            $(".header").removeClass("nav-active");
            $('.banner').addClass('nav-banner');
         
        }
    });


});
//返回顶部
$(document).ready(function () {
     $(document).on("mouseenter", ".suspension .a", function () {
        var _this = $(this);
        var s = $(".suspension");
        var isService = _this.hasClass("h-service");
        var isServicePhone = _this.hasClass("h-service-phone");
        var isServiceCar = _this.hasClass("h-service-car");
        var isServiceSell = _this.hasClass("h-service-sell");
        var isQrcode = _this.hasClass("h-qrcode");
        if (isService) { s.find(".d-service").show().siblings(".d").hide(); }
        if (isServicePhone) { s.find(".d-service-phone").show().siblings(".d").hide(); }
        if (isServiceCar) { s.find(".d-service-car").show().siblings(".d").hide(); }
        if (isServiceSell) { s.find(".d-service-sell").show().siblings(".d").hide(); }
        if (isQrcode) { s.find(".d-qrcode").show().siblings(".d").hide(); }
    });
    $(document).on("mouseleave", ".suspension, .suspension .h-top", function () {
        $(".suspension").find(".d").hide();
    });
    $(document).on("mouseenter", ".suspension .h-top", function () {
        $(".suspension").find(".d").hide();
    });
    $(document).on("click", ".suspension .h-top", function () {
        $("html,body").animate({ scrollTop: 0 });
    });
    $(window).scroll(function () {
        var st = $(document).scrollTop();
        var $top = $(".suspension .h-top");
        if (st > 400) {
            $top.css({ display: 'block' });
            $(".ad").show();
        } else {
            if ($top.is(":visible")) {
                $top.hide();
            }
            $(".ad").hide();
        }
    });

    $('.nav-menu').superfish({
        animation: { opacity: 'show' },
        speed: 50
    });

    if ($('#nav-menu-container').length) {
        var $mobile_nav = $('#nav-menu-container').clone().prop({ id: 'mobile-nav' });
        $mobile_nav.find('> ul').attr({ 'class': '', 'id': '' });
        $('body').append($mobile_nav);
        $('body').prepend('<button type="button" id="mobile-nav-toggle"><i class="fa fa-bars"></i></button>');
        $('body').append('<div id="mobile-body-overly"></div>');
        $('#mobile-nav').find('.menu-has-children').prepend('<i class="fa fa-chevron-down"></i>');

        $(document).on('click', '.menu-has-children i', function (e) {
            $(this).next().toggleClass('menu-item-active');
            $(this).nextAll('ul').eq(0).slideToggle();
            $(this).toggleClass("fa-chevron-up fa-chevron-down");
        });

        $(document).on('click', '#mobile-nav-toggle', function (e) {
            $('body').toggleClass('mobile-nav-active');
            $('#mobile-nav-toggle i').toggleClass('fa-times fa-bars');
            $('#mobile-body-overly').toggle();
        });

        $(document).click(function (e) {
            var container = $("#mobile-nav, #mobile-nav-toggle");
            if (!container.is(e.target) && container.has(e.target).length === 0) {
                if ($('body').hasClass('mobile-nav-active')) {
                    $('body').removeClass('mobile-nav-active');
                    $('#mobile-nav-toggle i').toggleClass('fa-times fa-bars');
                    $('#mobile-body-overly').fadeOut();
                }
            }
        });
    } else if ($("#mobile-nav, #mobile-nav-toggle").length) {
        $("#mobile-nav, #mobile-nav-toggle").hide();
    }
});