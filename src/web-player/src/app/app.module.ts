import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { NgxYoutubePlayerModule } from 'ngx-youtube-player';
import { AppComponent } from './app.component';
import { AppPlayerComponent } from './components/app-player/app-player.component';
import { HttpClientModule, HttpClientJsonpModule } from '@angular/common/http';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ReactionComponent } from './components/reaction/reaction.component';
import { ScoreComponent } from './components/score/score.component';
import { ConfettiComponent } from './components/confetti/confetti.component';
import { StageComponent } from './components/stage/stage.component';
import { FormsModule } from '@angular/forms';
import { NextSongComponent } from './components/next-song/next-song.component';

@NgModule({
  declarations: [
    AppComponent,
    AppPlayerComponent,
    ReactionComponent,
    ScoreComponent,
    ConfettiComponent,
    StageComponent,
    NextSongComponent
  ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    NgxYoutubePlayerModule.forRoot(),
    HttpClientModule,
    HttpClientJsonpModule,
    FormsModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
