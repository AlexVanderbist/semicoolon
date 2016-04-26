<?php

namespace App;

use Illuminate\Foundation\Auth\User as Authenticatable;

class Project extends Authenticatable
{
    /**
     * The attributes that are mass assignable.
     *
     * @var array
     */
    protected $fillable = [
        'name', 
        'lat',
        'lng',
        'locationText',
        'stage_id',
        'thema_id',
        'project_creator'
    ];

    /**
     * The attributes excluded from the model's JSON form.
     *
     * @var array
     */
}
