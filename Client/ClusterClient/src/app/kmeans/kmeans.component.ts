import { Component, OnInit } from '@angular/core';
import {Centroid, Cluster, DataService} from "../data.service";

@Component({
  selector: 'app-kmeans',
  templateUrl: './kmeans.component.html',
  styleUrls: ['./kmeans.component.css']
})
export class KmeansComponent implements OnInit {

  haltCentroids: Centroid[];
  centroids: Centroid[];

  constructor(private dataService: DataService) { }

  ngOnInit() {
    this.showKMeans();
    this.showKMeansHalt();
  }

  showKMeans() {

    this.dataService.getKMeans()
      .subscribe((data: Centroid[]) =>
      {
        console.log(data);
        this.centroids = data;
      });
  }

  showKMeansHalt() {

    this.dataService.getKMeans()
      .subscribe((data: Centroid[]) =>
      {
        console.log(data);
        this.haltCentroids = data;
      });
  }

}
