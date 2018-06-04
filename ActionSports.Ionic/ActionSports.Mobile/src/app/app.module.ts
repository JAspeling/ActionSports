import { BrowserModule } from '@angular/platform-browser';
import { ErrorHandler, NgModule } from '@angular/core';
import { IonicApp, IonicErrorHandler, IonicModule } from 'ionic-angular';
import { SplashScreen } from '@ionic-native/splash-screen';
import { StatusBar } from '@ionic-native/status-bar';
import { HttpClientModule } from '@angular/common/http';


import { MyApp } from './app.component';
import { VenuePage } from '../pages/venues/venue';
import { VenuesService } from '../services/venuesService';
import { LeaguePage } from '../pages/leagues/leagues';
import { LeaguesService } from '../services/leaguesService';

@NgModule({
  declarations: [
    MyApp,
    VenuePage,
    LeaguePage
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    IonicModule.forRoot(MyApp)
  ],
  bootstrap: [IonicApp],
  entryComponents: [
    MyApp,
    VenuePage,
    LeaguePage
  ],
  providers: [
    StatusBar,
    SplashScreen,
    VenuesService,
    LeaguesService,
    {provide: ErrorHandler, useClass: IonicErrorHandler}
  ]
})
export class AppModule {}
