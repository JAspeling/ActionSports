import { VenuePage } from './../pages/venues/venue';
import { Component } from '@angular/core';
import { Platform } from 'ionic-angular';
import { StatusBar } from '@ionic-native/status-bar';
import { SplashScreen } from '@ionic-native/splash-screen';
import { Storage } from '@ionic/storage';
import { AppState } from '../classes/AppState';

@Component({  
  templateUrl: 'app.html'
})
export class MyApp {
  rootPage:any = VenuePage;

  constructor(platform: Platform, statusBar: StatusBar, splashScreen: SplashScreen, private storage: Storage) {

    // storage.set('api', 'http://action.jaspeling.co.za/ActionSports.API');
    // AppState.basePage = 'http://action.jaspeling.co.za/ActionSports.API/api'
    AppState.basePage = 'http://localhost:5000/api'

    storage.forEach((value, key, index) => {
      console.log(`${key} - ${value}`);
    }).then(() => {
      storage.get('api').then((val) => {
      
        console.log('Using api', val);
        platform.ready().then(() => {
          // Okay, so the platform is ready and our plugins are available.
          // Here you can do any higher level native things you might need.
          statusBar.styleDefault();
          splashScreen.hide();
        });
      });
    });
    
  }
}

