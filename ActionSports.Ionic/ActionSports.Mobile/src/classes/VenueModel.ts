export class VenueModel {
    constructor(init? : Partial<VenueModel>) {
        Object.assign(this, init);
    }
    
    title: string;
    href: string;
}