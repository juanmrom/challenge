import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatIconModule } from '@angular/material/icon';
import { MatSidenavModule } from '@angular/material/sidenav';
import { MatListModule } from '@angular/material/list';
import { MatButtonModule } from '@angular/material/button';
import { MatTableModule } from '@angular/material/table';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatSelectModule } from '@angular/material/select';
import { MatInputModule } from '@angular/material/input';


@NgModule({
  declarations: [],
  imports: [
    CommonModule
    , MatToolbarModule
    , MatIconModule
    , MatSidenavModule
    , MatListModule
    , MatButtonModule
    , MatTableModule
    , MatPaginatorModule
    , MatCheckboxModule
    , MatSelectModule
    , MatInputModule
  ],
  exports:[
    MatToolbarModule
    , MatIconModule
    , MatSidenavModule
    , MatListModule
    , MatButtonModule
    , MatTableModule
    , MatPaginatorModule
    , MatCheckboxModule
    , MatSelectModule
    , MatInputModule
  ]
})
export class MaterialModule { }
