import { Component, Input } from "@angular/core";

@Component({
    selector: 'main-menu',
    templateUrl: './main-menu.html'
})
export class MainMenuComponent {
    @Input('content') content: any;
}