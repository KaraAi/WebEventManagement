

    //Close Popups and Fade Layer
    $('a.close1, #fade').live('click', function () { //When clicking on the close or fade layer...   

        $('#fade , .popup_block').fadeOut(function () {
            $('#fade, a.close1').remove();
            var video = $("#myVideo");
            video.attr("src", "");

            //window.location.href = window.location.href; //vu.nt
        }); //fade them both out

        $('#fade , .popup_block1').fadeOut(function () {
            $('#fade, a.close1').remove();
        });

        $('.popup_notification').fadeOut(function () {
            $('a.close1').remove();
        });

        return false;
    });

    $('a.linkscontent, #fade').live('click', function () {
        $('#fade , .popup_block1').fadeOut(function () {
            $('#fade, a.close1').remove();
        });

    });

    $('div.popup_notification, #fade').live('click', function () {
        $('.popup_notification').fadeOut(function () {
            $('a.close1').remove();
        });

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