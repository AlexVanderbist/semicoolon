<?php

namespace App;

use Illuminate\Database\Eloquent\Model;

class Stage extends Model
{
    protected $fillable = [
        'name', 
        'description',
        'startdate', 
        'enddate',
        'allow_input'
    ];

    protected $dates = ['startdate', 'enddate'];
}
