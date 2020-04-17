import { Component, OnInit, Output, Input } from '@angular/core';

@Component({
  selector: 'app-reaction',
  templateUrl: './reaction.component.html',
  styleUrls: ['./reaction.component.scss'],
})
export class ReactionComponent implements OnInit {

  constructor() {
    this.key = this.getRandomInt(1, 1000);
  }

  @Input() emoji: string;
  key;

  ngOnInit() {
  }

  getClass() {
    return `emoji-${this.key}`;
  }

  getRandomInt(min, max) {
    min = Math.ceil(min);
    max = Math.floor(max);
    return Math.floor(Math.random() * (max - min + 1)) + min;
  }
}
