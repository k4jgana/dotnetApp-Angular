import { RouterModule } from "@angular/router";
import { Checkout } from "../checkout/checkout.component";
import { LoginPage } from "../login/loginPage.component";
import { AuthActivator } from "../services/authActivator.service";
import { ShopPage } from "../shop/shopPage.component";

const routes =
    [
        { path: "", component: ShopPage },
        { path: "checkout", component: Checkout, canActivate: [AuthActivator] },
        { path: "login", component: LoginPage },
        {path:"**",redirectTo:"/"}

    ];

const router = RouterModule.forRoot(routes, {useHash:false});
export default router;