import { Component, OnInit, Input } from '@angular/core';

@Component({
  selector: 'app-stage',
  templateUrl: './stage.component.html',
  styleUrls: ['./stage.component.scss']
})
export class StageComponent implements OnInit {

  @Input() open = false;

  constructor() { }

  ngOnInit() {
  }

}
