import {autoinject} from 'aurelia-framework';
import { Router } from 'aurelia-router';
import shoppingCart from "../../shoppingCart";

@autoinject()
export class Confirmation {
  constructor(private router: Router) {
  }

  done() {
    shoppingCart.clear();
    this.router.navigate('checkout/confirmation');
  }
}
