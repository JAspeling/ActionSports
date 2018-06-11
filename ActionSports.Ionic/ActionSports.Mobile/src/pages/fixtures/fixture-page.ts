import { BasePage } from "../base-page";
import { Component } from "@angular/core";
import { NavParams, LoadingController, NavController, ToastController } from "ionic-angular";

@Component({
    selector: 'fixture-page',
    templateUrl: './fixture-page.html'
})
export class FixturePage extends BasePage {
    constructor(
        public loadingCtrl: LoadingController,
        public navParams: NavParams,
        public navCtrl: NavController,
        public toastCtrl: ToastController) {
        super(loadingCtrl, navParams, navCtrl, toastCtrl);
    }
}