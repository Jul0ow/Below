const allRonds = document.querySelectorAll('.rond');
const allboxes = document.querySelectorAll('.box');

const controller = new ScrollMagic.Controller()

allboxes.forEach(box => {
  for ( i = 0; i < allRonds.length; i++) {
    if (allRonds[i].getAttribute('data-anim') === box.getAttribute('data-anim')){
      let tween = gsap.from(box,{y: -50, opacity: 0,duration: 0.5}) /*animation*/

      let scene = new ScrollMagic.Scene({
        triggerElement: allRonds[i],
        reverse: false /* ne disparait pas quand on scroll vers le haut*/
      })
        .setTween(tween)
        //.addIndicators() /*ajout des indications sur la droite*/
        .addTo(controller)
    }
  }
})
