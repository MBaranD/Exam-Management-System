let text = document.getElementsByClassName("text");
let sun = document.getElementsByClassName("sun");
let school = document.getElementsByClassName("school");
let hat1 = document.getElementsByClassName("hat1");
let hat2 = document.getElementsByClassName("hat2");
let hat3 = document.getElementsByClassName("hat3");

window.addEventListener("scroll", () => {
    let value = Math.min(window.scrollY, 400);

    for (let i = 0; i < text.length; i++) {
        text[i].style.marginTop = value * 2.5 + "px";
    }

    for (let i = 0; i < school.length; i++) {
        school[i].style.transform = "scale(" + (1 + value / 1000) + ")";
    }

    for (let i = 0; i < hat1.length; i++) {
        hat1[i].style.right = value * -2 + "px";
        hat1[i].style.transform = "scale(" + (1 + value / 1000) + ")";
    }

    for (let i = 0; i < hat2.length; i++) {
        hat2[i].style.left = value * -1.5 + "px";
        hat2[i].style.transform = "scale(" + (1 + value / 1000) + ")";
    }

    for (let i = 0; i < hat3.length; i++) {
        hat3[i].style.right = value * -1.5 + "px";
        hat3[i].style.transform = "scale(" + (1 + value / 1000) + ")";
    }
});