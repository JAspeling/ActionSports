import { VenuesService } from './../../services/venuesService';
import { BasePage } from './../base-page';
import { Component } from '@angular/core';
import { LeaguePage } from '../leagues/leagues';
import { Loading, LoadingController, NavController, NavParams, ToastController } from 'ionic-angular';
import { VenueModel } from './../../classes/VenueModel';

@Component({
    selector: 'page-venue',
    templateUrl: 'venue.html'
})
export class VenuePage extends BasePage{
    venues: VenueModel[] = [];

    constructor(
        public loadingCtrl: LoadingController,
        public navParams: NavParams,
        public navCtrl: NavController,
        public venuesService: VenuesService,
        public toastCtrl: ToastController
    ) {
        super(loadingCtrl, navParams, navCtrl, toastCtrl);
        this.createLoading('Retrieving Venues...');

        this.loader.present().then(() => {
            this.venuesService.getVenues()
            .then((venues: VenueModel[]) => {
                venues.forEach(venue => {
                    this.venues.push(venue);
                });
                this.loader.dismiss();
            }, err => {
                this.loader.dismiss();
                this.presentToast('Failed to retrieve Venues');
            });
        });
    }

    navigateToVenue(venue: VenueModel) {
        var navOptions = {
            animation: 'wd-transition'
        };
        this.navCtrl.push(LeaguePage, { venue: venue }, navOptions);
    }
}
