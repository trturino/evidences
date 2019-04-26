import {
  animate,
  state,
  style,
  transition,
  trigger,
  group,
  keyframes
} from '@angular/animations';

const generateYOutputRange = () => {
  return Math.random() * 300;
}

const generateScaleOutputRange = () => {
  return Math.random() + 1;
}

const generateParams = () => {
  return {
    params: {
      transformY: generateYOutputRange(),
      scale: generateScaleOutputRange()
    }
  }
}

export const emojiAnim = trigger('emojiAnim', [
  state('off', style({
    left: `${window.innerWidth}px`,
  })),
  state('on', style({
    left: `0px`,
    opacity: 0
  })),
  transition('off => on', animate('7000ms cubic-bezier(0.550, 0.055, 0.675, 0.190)', keyframes([
    style({left: `${window.innerWidth}px`,  opacity: 1, transform: `translateY({{transformY}}px) rotate(0deg) scale({{scale}})`, offset: 0}, generateParams()),
    style({left: `${window.innerWidth - (window.innerWidth/5)}px`, opacity: 1, transform: `translateY({{transformY}}px) rotate(-20deg) scale({{scale}})`, offset: 0.2}, generateParams()),
    style({left: `${window.innerWidth - (window.innerWidth/5)*2}px`, opacity: 1, transform: `translateY({{transformY}}px) rotate(0deg) scale({{scale}})`, offset: 0.4}, generateParams()),
    style({left: `${window.innerWidth - (window.innerWidth/5)*3}px`, opacity: .8, transform: `translateY({{transformY}}px) rotate(20deg) scale({{scale}})`, offset: 0.6}, generateParams()),
    style({left: `${window.innerWidth - (window.innerWidth/5)*4}px`, opacity: .5, transform: `translateY({{transformY}}px) rotate(0deg) scale({{scale}})`, offset: 0.8}, generateParams()),
    style({left: `0px`, opacity: 0, transform: `translateY({{transformY}}px) rotate(-20deg) scale({{scale}})`, offset: 1}, generateParams())
  ]))),
]);
