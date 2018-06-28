"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};
Object.defineProperty(exports, "__esModule", { value: true });
var core_1 = require("@angular/core");
var http_1 = require("@angular/common/http");
var Bet_1 = require("../models/Bet");
var auth_service_1 = require("../authservice/auth.service");
var router_1 = require("@angular/router");
var FixtureComponent = /** @class */ (function () {
    function FixtureComponent(http, auth, router) {
        this.http = http;
        this.auth = auth;
        this.router = router;
        this.betClosed = false;
    }
    // tslint:disable-next-line:use-life-cycle-interface
    FixtureComponent.prototype.ngOnChanges = function () {
        var _this = this;
        var h = this.fixture.date.getHours() - 3;
        var betDate = new Date(this.fixture.date);
        betDate.setHours(h);
        this.auth.connectedUser().then(function (x) {
            _this.http.get('http://localhost:2323/api/bet/fixture/' + _this.fixture.id + '/player/' + x.userName).toPromise().then(function (data) {
                _this.existingBet = data;
                _this.fixture.date.setHours(h);
                _this.betClosed = betDate < new Date() || _this.existingBet !== null;
            });
        });
    };
    FixtureComponent.prototype.timeToString = function (date) {
        return date.getHours() + ':' + date.getMinutes() + '0';
    };
    FixtureComponent.prototype.saveBet = function () {
        var _this = this;
        this.auth.connectedUser().then(function (data) {
            _this.http.post('http://localhost:2323/api/bet/', new Bet_1.Bet(null, _this.fixture.id, false, data.id, _this.fixture.scoreTeam1, _this.fixture.scoreTeam2, null, new Date(), false, null, null), { headers: { 'Content-Type': 'application/json; charset=utf-8' } })
                .toPromise()
                .then(function (x) {
                _this.router.navigateByUrl('/competition');
            });
        });
    };
    __decorate([
        core_1.Input(),
        __metadata("design:type", Object)
    ], FixtureComponent.prototype, "fixture", void 0);
    FixtureComponent = __decorate([
        core_1.Component({
            // tslint:disable-next-line:component-selector
            selector: 'fixture',
            templateUrl: './fixture.component.html'
        }),
        __metadata("design:paramtypes", [http_1.HttpClient, auth_service_1.AuthService, router_1.Router])
    ], FixtureComponent);
    return FixtureComponent;
}());
exports.FixtureComponent = FixtureComponent;
//# sourceMappingURL=fixture.component.js.map