<?php

namespace App;

use Illuminate\Foundation\Auth\User as Authenticatable;

class Theme extends Authenticatable
{
    /**
     * The attributes that are mass assignable.
     *
     * @var array
     */
    protected $fillable = [
        'name', 
        'hex_color'
    ];

    /**
     * The attributes excluded from the model's JSON form.
     *
     * @var array
     */
}
