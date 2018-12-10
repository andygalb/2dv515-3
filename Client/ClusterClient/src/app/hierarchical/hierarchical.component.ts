import { Component, OnInit } from '@angular/core';
import {Centroid, Cluster, DataService} from "../data.service";


//declare var jquery:any;
//declare var $ :any;
import * as $ from 'jquery';


@Component({
  selector: 'app-hierarchical',
  templateUrl: './hierarchical.component.html',
  styleUrls: ['./hierarchical.component.css']
})
export class HierarchicalComponent implements OnInit {

  title = 'Cluster';
  clusters: Cluster[];

  constructor(private dataService: DataService) { }

  ngOnInit() {
    this.showHierarchical();
  }


  showHierarchical() {

    this.dataService.getHierarchicalCluster()
      .subscribe((data: Cluster[]) =>
      {
        console.log(data);
        this.clusters = data;
      });
  }


}
