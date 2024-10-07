
$(document).ready(function () {   
    //When you click on a link with class of poplight and the href starts with a # 
    $('a.poplight[href^=#]').click(function () {
        alert(111);
        var popURL = $(this).attr('href'); //Get Popup href to define size

        var relUrl = $(this).attr('rel');
        var relClassName = relUrl.split('|');
        var popID = relClassName[0];

        var urlVideo = relClassName[1];
        var video = $("#myVideo");
        //alert(video);
        video.attr("src", urlVideo);

        //Pull Query & Variables from href URL
        var query = popURL.split('?');
        var dim = query[1].split('&');
        var popWidth = dim[0].split('=')[1]; //Gets the first query string value

        //Fade in the Popup and add close button

        $('#' + popID).fadeIn().css({ 'width': 234 }).prepend('<a href="#" class="close"><img src="images/close_icon.png" class="btn_close" title="Close Window" alt="Close" /></a>');
        $('#' + popID).fadeIn().css({ 'width': Number(popWidth) }).prepend('');

        //Define margin for center alignment (vertical + horizontal) - we add 80 to the height/width to accomodate for the padding + border width defined in the css

        var popMargTop = ($('#' + popID).height() + 40) / 2;
        var popMargLeft = ($('#' + popID).width() + 10) / 2;

        //Apply Margin to Popup
        $('#' + popID).css({
            'margin-top': -popMargTop,
            'margin-left': -popMargLeft
        });

        //Fade in Background
        $('body').append('<div id="fade"></div>'); //Add the fade layer to bottom of the body tag.
        $('#fade').css({ 'filter': 'alpha(opacity=80)' }).fadeIn(); //Fade in the fade layer 

        return false;
    });

    //Close Popups and Fade Layer
    $('a.close, #fade').live('click', function () { //When clicking on the close or fade layer...   

        $('#fade , .popup_block').fadeOut(function () {
            $('#fade, a.close').remove();
            var video = $("#myVideo");
            video.attr("src", "");
            //window.location.href = window.location.href; //vu.nt
        }); //fade them both out

        return false;
    });

});
//$("#form1").validate({
//    submitHandler: function (form) {
//        // some other code
//        // maybe disabling submit button
//        // then:
//       // alert(1);
//       // $(form).submit();
//    }
//});