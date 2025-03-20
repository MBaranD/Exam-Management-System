const wrapper = document.querySelector(".wrapper");
const HomePageLink = document.querySelector(".HomePageLink");
const RegisterPageLink = document.querySelector(".RegisterPageLink");
const loginbutton = document.querySelector(".loginbutton");
const iconClose = document.querySelector(".iconClose");

//
RegisterPageLink.addEventListener("click", () => {
  wrapper.classList.add("active");
});

HomePageLink.addEventListener("click", () => {
  wrapper.classList.remove("active");
});

loginbutton.addEventListener("click", () => {
  wrapper.classList.add("active-popup");
});

//LoginPageCloseButton
iconClose.addEventListener("click", () => {
  wrapper.classList.remove("active-popup");
});
