$(window).on('load resize', function() {
    //Add/remove class based on browser size when load/resize
	var w = $(window).width();

    if(w >= 1400) {
	    // if larger 
	    $('#docs-sidebar').addClass('sidebar-visible').removeClass('sidebar-hidden');
	} else {
	    // if smaller
	    $('#docs-sidebar').addClass('sidebar-hidden').removeClass('sidebar-visible');
	}
});


$(document).ready(function() {
	
	/* ====== Toggle Sidebar ======= */
	
	$('#docs-sidebar-toggler').on('click', function(){
	
		if ( $('#docs-sidebar').hasClass('sidebar-visible') ) {

			  $("#docs-sidebar").removeClass('sidebar-visible').addClass('sidebar-hidden');
			
			
		} else {

			  $("#docs-sidebar").removeClass('sidebar-hidden').addClass('sidebar-visible');
			
		}
			
    });
    

    /* ====== Активируем меню прокрутки ===== */
    $('body').scrollspy({target: '#docs-nav', offset: 100});
    
    
	
    /* ===== Плавная прокрутка ====== */
	$('#docs-inner-navigation a.scrollto').on('click', function(e){
        //store hash
        var target = this.hash;    
        e.preventDefault();
		$('body').scrollTo(target, 800, {offset: -69, 'axis':'y'});
		
		//Свернуть боковую панель после нажатия
		if ($('#docs-sidebar').hasClass('sidebar-visible') && $(window).width() < 1400){
			$('#docs-sidebar').removeClass('sidebar-visible').addClass('slidebar-hidden');
		}
	});
	

	
	/* Плавная прокрутка при загрузке страницы, если URL имеет # */
	if(window.location.hash) {
		var urlhash = window.location.hash;
		$('body').scrollTo(urlhash, 800, {offset: -69, 'axis':'y'});
	}
	
	
	/* Bootstrap lightbox */

    $(document).delegate('*[data-toggle="lightbox"]', 'click', function(e) {
        e.preventDefault();
        $(this).ekkoLightbox();
    }); 

});