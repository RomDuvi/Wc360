"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
Object.defineProperty(exports, "__esModule", { value: true });
var platform_browser_1 = require("@angular/platform-browser");
var core_1 = require("@angular/core");
var router_1 = require("@angular/router");
var app_component_1 = require("./app.component");
var fixture_component_1 = require("../fixture/fixture.component");
var round_component_1 = require("../round/round.component");
var navmenu_component_1 = require("../navmenu/navmenu.component");
var home_component_1 = require("../home/home.component");
var http_1 = require("@angular/common/http");
var forms_1 = require("@angular/forms");
var ranking_component_1 = require("../ranking/ranking.component");
var bet_component_1 = require("../bet/bet.component");
var token_interceptor_1 = require("../authservice/token.interceptor");
var login_component_1 = require("../login/login.component");
var auth_service_1 = require("../authservice/auth.service");
var loginactivate_guard_1 = require("../authservice/loginactivate.guard");
var roundteam_component_1 = require("../bet/roundteam/roundteam.component");
var winnerteam_component_1 = require("../bet/winnerteam/winnerteam.component");
var qualifteam_component_1 = require("../bet/qualifteam/qualifteam.component");
var appRoutes = [
    { path: '', redirectTo: 'home', pathMatch: 'full', canActivate: [loginactivate_guard_1.LoginActivate] },
    { path: 'login', component: login_component_1.LoginComponent },
    { path: 'bet', component: bet_component_1.BetComponent, canActivate: [loginactivate_guard_1.LoginActivate] },
    { path: 'ranking', component: ranking_component_1.RankingComponent, canActivate: [loginactivate_guard_1.LoginActivate] },
    { path: 'home', component: home_component_1.HomeComponent, canActivate: [loginactivate_guard_1.LoginActivate] },
    { path: '**', component: login_component_1.LoginComponent }
];
var AppModule = /** @class */ (function () {
    function AppModule() {
    }
    AppModule = __decorate([
        core_1.NgModule({
            declarations: [
                app_component_1.AppComponent,
                fixture_component_1.FixtureComponent,
                round_component_1.RoundComponent,
                navmenu_component_1.NavMenuComponent,
                home_component_1.HomeComponent,
                bet_component_1.BetComponent,
                ranking_component_1.RankingComponent,
                login_component_1.LoginComponent,
                roundteam_component_1.RoundTeamComponent,
                winnerteam_component_1.WinnerTeamComponent,
                qualifteam_component_1.QualifTeamComponent
            ],
            imports: [
                platform_browser_1.BrowserModule,
                forms_1.FormsModule,
                http_1.HttpClientModule,
                router_1.RouterModule.forRoot(appRoutes)
            ],
            providers: [
                {
                    provide: http_1.HTTP_INTERCEPTORS,
                    useClass: token_interceptor_1.TokenInterceptor,
                    multi: true
                },
                {
                    provide: auth_service_1.AuthService,
                    useClass: auth_service_1.AuthService
                },
                {
                    provide: loginactivate_guard_1.LoginActivate,
                    useClass: loginactivate_guard_1.LoginActivate
                }
            ],
            bootstrap: [app_component_1.AppComponent]
        })
    ], AppModule);
    return AppModule;
}());
exports.AppModule = AppModule;
//# sourceMappingURL=app.module.js.map