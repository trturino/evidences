import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';

@Component({
  selector: 'app-next-song',
  templateUrl: './next-song.component.html',
  styleUrls: ['./next-song.component.scss']
})
export class NextSongComponent implements OnInit {

  @Input() nextSong;
  @Input() isVisible = true;
  @Output() playSong: EventEmitter<any>;

  constructor() {
    this.playSong = new EventEmitter();
   }

  ngOnInit() {
  }

  play() {
    this.playSong.emit();
  }

}
