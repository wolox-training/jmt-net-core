// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$(function() {
    $('.hola-button').click(function(){ 
        alert("HOLA");
    });
    var commentStr = $();
    $.ajax {
        url:"localhost/add",
        data: {comment: commentStr},
        success: function(){
            
        },
        error: function(){

        }
    }
});