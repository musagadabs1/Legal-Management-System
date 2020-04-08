$(function() {
	"use strict";
	skinChanger();
    initSparkline();
    
    setTimeout(function() {
        $('.page-loader-wrapper').fadeOut();
    }, 50);
});

// Sparkline
function initSparkline() {

	$(".sparkline").each(function() {
		var $this = $(this);
		$this.sparkline('html', $this.data());
    });
    
    // block-header bar chart js
    $('.bh_visitors').sparkline('html', {
        type: 'bar',
        height: '42px',
        barColor: '#a27ce6',
        barWidth: 5,
    });
    $('.bh_visits').sparkline('html', {
        type: 'bar',
        height: '42px',
        barColor: '#0078ff',
        barWidth: 5,
    });
    $('.bh_chats').sparkline('html', {
        type: 'bar',
        height: '42px',
        barColor: '#50d38a',
        barWidth: 5,
    });
}
//Skin changer
function skinChanger() {
	$('.choose-skin li').on('click', function() {
	    var $body = $('body');
	    var $this = $(this);
  
	    var existTheme = $('.choose-skin li.active').data('theme');
	    $('.choose-skin li').removeClass('active');
	    $body.removeClass('theme-' + existTheme);
	    $this.addClass('active');
	    $body.addClass('theme-' + $this.data('theme'));
	});
}

$(document).ready(function() {

	// sidebar navigation
	$('.main-menu').metisMenu();

	// sidebar nav scrolling
	$('#left-sidebar .sidebar-scroll').slimScroll({
		height: 'calc(100vh - 65px)',
		wheelStep: 10,
		touchScrollStep: 50,
		color: '#efefef',
		size: '2px',
		borderRadius: '3px',
		alwaysVisible: false,
		position: 'right',
	});

	// cwidget scroll
	$('.cwidget-scroll').slimScroll({
		height: '263px',
		wheelStep: 10,
		touchScrollStep: 50,
		color: '#efefef',
		size: '2px',
		borderRadius: '3px',
		alwaysVisible: false,
		position: 'right',
	});

	// Body Fullwidth class Add/Remove
	$(window).bind("resize", function () {
		if ($(this).width() > 1200) {
			$('body').addClass('layout-fullwidth');
		} else {
			$('body').removeClass('layout-fullwidth');
		}
	}).trigger('resize');

	// toggle fullwidth layout
	$('.btn-toggle-fullwidth').on('click', function() {
		if(!$('body').hasClass('layout-fullwidth')) {
			console.log('if');
			$('body').addClass('layout-fullwidth');
			$('#left-sidebar').addClass('mini-sidebar');
			$(this).find(".fa").toggleClass('fa-arrow-right fa-arrow-left');

		} else {
			console.log('else');
			$('body').removeClass('layout-fullwidth');
			$('#left-sidebar').removeClass('mini-sidebar');
			$(this).find(".fa").toggleClass('fa-arrow-right fa-arrow-left');
		}
	});

    // Mini sidebar hover js
	$( "#left-sidebar" ).mouseleave(function() {
		$("#left-sidebar #menu").addClass('active show').siblings().removeClass("active show");
		$("#left-sidebar .nav-tabs .nav-item .nav-link").removeClass("active show");
		$("#left-sidebar .nav-tabs .nav-item:first .nav-link").addClass("active show");
    });
    
	$("#left-sidebar").hover(function () {
		$(this).toggleClass("mini-sidebar-hover");
	});

	// off-canvas menu toggle
	$('.btn-toggle-offcanvas').on('click', function() {
		$('body').toggleClass('offcanvas-active');
	});

	$('#main-content').on('click', function() {
		$('body').removeClass('offcanvas-active');
	});

	// adding effect dropdown menu
	$('.dropdown').on('show.bs.dropdown', function() {
		$(this).find('.dropdown-menu').first().stop(true, true).animate({
			top: '100%'
		}, 200);
	});

	$('.dropdown').on('hide.bs.dropdown', function() {
		$(this).find('.dropdown-menu').first().stop(true, true).animate({
			top: '80%'
		}, 200);
	});

	// navbar search form
	$('.navbar-form.search-form input[type="text"]')
	.on('focus', function() {
		$(this).animate({
			width: '+=50px'
		}, 300);
	})
	.on('focusout', function() {
		$(this).animate({
			width: '-=50px'
		}, 300);
	});

	// Bootstrap tooltip init
	if($('[data-toggle="tooltip"]').length > 0) {
		$('[data-toggle="tooltip"]').tooltip();
	}

	if($('[data-toggle="popover"]').length > 0) {
		$('[data-toggle="popover"]').popover();
	}

	$(window).on('load', function() {
		// for shorter main content
		if($('#main-content').height() < $('#left-sidebar').height()) {
			$('#main-content').css('min-height', $('#left-sidebar').innerHeight() - $('footer').innerHeight());
		}
	});

	$(window).on('load', function() {
		// for shorter main content
		if($('#main-content').height() < $('#left-sidebar').height()) {
			$('#main-content').css('min-height', $('#left-sidebar').innerHeight() - $('footer').innerHeight());
		}
	});

	$(window).on('load resize', function() {
		if($(window).innerWidth() < 420) {
			$('.navbar-brand logo.svg').attr('src', '../assets/images/logo-icon.svg');
		} else {
			$('.navbar-brand logo-icon.svg').attr('src', '../assets/images/logo.svg');
		}
    });
    
    // Select all checkbox
    $('.select-all').on('click',function(){
    
        if(this.checked){
            $(this).parents('table').find('.checkbox-tick').each(function(){
            this.checked = true;
            });
        }else{
            $(this).parents('table').find('.checkbox-tick').each(function(){
            this.checked = false;
            });
        }
        });

        $('.checkbox-tick').on('click',function(){   
        if($(this).parents('table').find('.checkbox-tick:checked').length == $(this).parents('table').find('.checkbox-tick').length){
            $(this).parents('table').find('.select-all').prop('checked',true);
        }else{
            $(this).parents('table').find('.select-all').prop('checked',false);
        }
    });

});

// toggle function
$.fn.clickToggle = function( f1, f2 ) {
	return this.each( function() {
		var clicked = false;
		$(this).bind('click', function() {
			if(clicked) {
				clicked = false;
				return f2.apply(this, arguments);
			}

			clicked = true;
			return f1.apply(this, arguments);
		});
	});

};

