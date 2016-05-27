<?php

namespace App\Providers;

use Illuminate\Support\ServiceProvider;

class AppServiceProvider extends ServiceProvider
{
    /**
     * Bootstrap any application services.
     *
     * @return void
     */
    public function boot()
    {
        view()->composer(
            ['layouts.auth', 'layouts.backend'], 'App\ViewComposers\AddStatusMessage'
        );
        view()->composer(
            ['layouts.*'], 'App\ViewComposers\AddUser'
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
