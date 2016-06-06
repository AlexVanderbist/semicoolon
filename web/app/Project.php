<?php

namespace App;

use Illuminate\Database\Eloquent\Model;
use Carbon;

class Project extends Model
{
    /**
     * The attributes that are mass assignable.
     *
     * @var array
     */
    protected $fillable = [
        'name', 
        'description',
        'lat',
        'lng',
        'radius',
        'locationText',
        'theme_id',
        'project_creator',
        'youtube_url',
        'comment_deadline'
    ];

    protected $appends = ['header_image', 'youtube_id', 'check_deadline'];

    public function creator()
    {
        return $this->belongsTo(User::class, 'project_creator');
    }

    public function theme()
    {
        return $this->belongsTo(Theme::class);
    }

    public function stages()
    {
        return $this->hasMany(Stage::class)->orderBy('startdate', 'asc');
    }

    public function proposals()
    {
        return $this->hasMany(Proposal::class);
    }

    public function images()
    {
        return $this->hasMany(ProjectImage::class);
    }

    public function opinions()
    {
        return $this->hasMany(Opinion::class);
    }

    public function setYoutubeUrlAttribute($value) {
        parse_str( parse_url( $value, PHP_URL_QUERY ), $id ); //gets the youtube ID
        $this->attributes['youtube_url'] = $id['v'];
    }

    public function getYoutubeUrlAttribute ($value) {
        return 'https://www.youtube.com/watch?v=' . $value;
    }

    public function getYoutubeIdAttribute () {
        return $this->attributes['youtube_url'];
    }

    public function getHeaderImageAttribute() {
        $image = $this->images()->where('is_header', 1)->first();
        if(!$image) $image = $this->images()->first();
        return $image;
    }

    public function getDeadlineAttribute()
    {
        if (!empty($this->attributes['comment_deadline'])) {
            return Carbon\Carbon::createFromFormat('Y-m-d H:i:s', $this->attributes['comment_deadline'])->format('Y-m-d');
        }
    }

    public function getCheckDeadlineAttribute()
    {
        if(Carbon\Carbon::parse($this->attributes['comment_deadline'])->diffInDays(Carbon\Carbon::now(), false) > 0) //When its past the comment_deadline
            {
                return false;
            }
        else 
            {
                return true;
            }
    }
}
