import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppComponent } from './app.component';
import { HomeComponent } from './home/home.component';
import {DataService} from './data.service';
import { HttpClientModule } from '@angular/common/http';

import {BrowserAnimationsModule} from '@angular/platform-browser/animations';
import {
  MatToolbarModule, MatSelectModule, MatGridListModule, MatCardModule,
  MatExpansionModule
} from '@angular/material';
import {RouterModule, Routes} from "@angular/router";
import { HierarchicalComponent } from './hierarchical/hierarchical.component';
import { KmeansComponent } from './kmeans/kmeans.component';
import { NgxJsonViewerModule } from 'ngx-json-viewer';

const appRoutes: Routes = [
  { path: '',   redirectTo: '/kmeans', pathMatch: 'full' },
  { path: 'hierarchical', component: HierarchicalComponent },
  { path: 'kmeans', component: KmeansComponent },
];


@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    HierarchicalComponent,
    KmeansComponent,
  ],
  imports: [
    NgxJsonViewerModule,
    BrowserModule,
    HttpClientModule,
    BrowserAnimationsModule,
    MatExpansionModule,
    MatToolbarModule,
    MatSelectModule,
    MatGridListModule,
    MatCardModule,
    RouterModule.forRoot(appRoutes)
  ],
  providers: [DataService],
  bootstrap: [AppComponent]
})
export class AppModule { }
