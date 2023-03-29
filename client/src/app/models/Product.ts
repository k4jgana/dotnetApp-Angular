export class Product {
    id: number = 0;
    category: string = '';
    size: string = '';
    price: number = 0;
    title: string = '';
    artDescription: string = '';
    artDating: string = '';
    artId: string = '';
    artist: string = '';
    artistBirthDate: Date = new Date();
    artistDeathDate: Date = new Date();
    artistNationality: string = '';

    constructor() {
        // initialize properties in the constructor
    }
}
