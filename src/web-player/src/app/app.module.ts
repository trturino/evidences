import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { NgxYoutubePlayerModule } from 'ngx-youtube-player';
import { AppComponent } from './app.component';
import { AppPlayerComponent } from './components/app-player/app-player.component';
import { HttpClientModule, HttpClientJsonpModule } from '@angular/common/http';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ReactionComponent } from './components/reaction/reaction.component';

@NgModule({
  declarations: [
    AppComponent,
    AppPlayerComponent,
    ReactionComponent
  ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    NgxYoutubePlayerModule.forRoot(),
    HttpClientModule,
    HttpClientJsonpModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
