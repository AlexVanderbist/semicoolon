<?php

namespace App\Providers;

use Illuminate\Support\ServiceProvider;
use Carbon\Carbon;

class AppServiceProvider extends ServiceProvider
{
    /**
     * Bootstrap any application services.
     *
     * @return void
     */
    public function boot()
    {
        Carbon::setLocale('nl');
        setlocale(LC_TIME, 'Dutch');
        view()->composer(
            ['layouts.auth', 'layouts.backend'], 'App\Http\ViewComposers\AddStatusMessage'
        );
    }

    /**
     * Register any application services.
     *
     * @return void
     */
    public function register()
    {
        //
    }
}
