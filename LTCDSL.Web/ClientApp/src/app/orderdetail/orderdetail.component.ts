import { Component, OnInit, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import * as $ from 'jquery';

@Component({
  selector: 'app-orderdetail',
  templateUrl: './orderdetail.component.html',
  styleUrls: ['./orderdetail.component.css']
})
export class OrderdetailComponent implements OnInit {

  dsDonHang: any = {};


  constructor(private http: HttpClient, @Inject('BASE_URL') baseUrl: string) { 
  }

  ngOnInit() {
  }

  submit(){
    var ma = $('#id').val();
    var para: any = {
      id: parseInt(ma),
      keyword: ""
    }    
    this.http.post('https://localhost:44377/api/De2/LayChiTietDonHang', para).subscribe(result => {
      var res: any = result;
      this.dsDonHang = res.data;
    }, error => console.error(error));
  }

}
