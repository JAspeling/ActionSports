import { VenuesService } from './../../services/venuesService';
import { BasePage } from './../base-page';
import { Component } from '@angular/core';
import { LeaguePage } from '../leagues/leagues';
import { Loading, LoadingController, NavController, NavParams, ToastController, MenuController } from 'ionic-angular';
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
        public toastCtrl: ToastController,
        public menu: MenuController
    ) {
        super(loadingCtrl, navParams, navCtrl, toastCtrl);
        this.createLoading('Retrieving Venues...');

        this.venues = [];
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

    refresh() {
        this.venues = [];
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

    toggleMenu() {
        const _menu = 'mainMenu';
        this.menu.enable(true, 'mainMenu');
    }
}
