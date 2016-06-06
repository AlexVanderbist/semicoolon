<?php

namespace App;

use Illuminate\Database\Eloquent\Model;

class UserNotification extends Model
{
	public $table = "user_notification";

    protected $fillable = [
        'user_id',
        'project_id'
    ];

    public function user() {
    	return $this->belongsTo(User::class);
    }
}
