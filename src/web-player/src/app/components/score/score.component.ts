import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-score',
  templateUrl: './score.component.html',
  styleUrls: ['./score.component.scss']
})
export class ScoreComponent implements OnInit {
  constructor() {
  }

  showScore = false;

  ngOnInit() {
    setTimeout(() => {
      this.showScore = true;
    }, 2000);
  }
}
