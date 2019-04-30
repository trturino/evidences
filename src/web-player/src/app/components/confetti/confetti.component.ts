import { Component, OnInit, NgZone } from '@angular/core';
import { ConfettiAnimation } from './animation/confettiAnimation';

@Component({
  selector: 'app-confetti',
  templateUrl: './confetti.component.html',
  styleUrls: ['./confetti.component.scss']
})
export class ConfettiComponent implements OnInit {
  animation: ConfettiAnimation;

  constructor(private zone: NgZone) { }

  ngOnInit() {
    this.zone.runOutsideAngular(() => {
      this.animation = new ConfettiAnimation('canvas');
      this.animation.Start();
    });
  }

}
