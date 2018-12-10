import {Component, OnInit} from '@angular/core';
import {DataService, Centroid, Cluster} from './data.service';


@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {

  title = 'Cluster';
  centroids: Centroid[];
  haltCentroids: Centroid[];
  clusters: Cluster[];

  ngOnInit(): void {
  }

  constructor(private dataService: DataService) {}



}

