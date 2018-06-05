import {
    LoadingController,
    Loading,
    NavParams,
    NavController
} from 'ionic-angular';

export class BasePage {
    loader: Loading;
    navOptions: {
        animation: string;
    };

    constructor(
        public loadingCtrl: LoadingController,
        public navParams: NavParams,
        public navCtrl: NavController
    ) {
        if (this.navOptions) this.navOptions.animation = 'wd-transition';
    }

    createLoading(message: string, spinner: string = 'dots') {
        this.loader = this.loadingCtrl.create({
            content: message,
            spinner: spinner
        });
    }
}
