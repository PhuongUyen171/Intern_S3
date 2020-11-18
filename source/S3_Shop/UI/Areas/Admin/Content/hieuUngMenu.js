﻿'use strict';


var tid = setInterval(function () {
    if (document.readyState !== 'complete') return;
    clearInterval(tid);
    var querySelector = document.querySelector.bind(document);

    var nav = document.querySelector('.vertical_nav');
    var wrapper = document.querySelector('.wrapper');

    var menu = document.getElementById("js-menu");
    var subnavs = menu.querySelectorAll('.menu--item__has_sub_menu');

    querySelector('.toggle_menu').onclick = function () {

        nav.classList.toggle('vertical_nav__opened');

        wrapper.classList.toggle('toggle-content');
    };

    querySelector('.collapse_menu').onclick = function () {

        nav.classList.toggle('vertical_nav__minify');

        wrapper.classList.toggle('wrapper__minify');

        for (var j = 0; j < subnavs.length; j++) {

            subnavs[j].classList.remove('menu--subitens__opened');
        }
    };

    for (var i = 0; i < subnavs.length; i++) {
        if (subnavs[i].classList.contains('menu--item__has_sub_menu')) {
            subnavs[i].addEventListener('click', function (e) {
                e.preventDefault();
                for (var j = 0; j < subnavs.length; j++) {
                    if (this != subnavs[j])
                        subnavs[j].classList.remove('menu--subitens__opened');
                }
                this.classList.toggle('menu--subitens__opened');
            }, false);
        }
    }
}, 100);