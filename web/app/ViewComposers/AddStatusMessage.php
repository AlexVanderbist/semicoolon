<?php

namespace App\Http\ViewComposers;

use Illuminate\View\View;

class AddStatusMessage 
{
	public function compose(View $view)
	{
		$view->with('status', session('status'));
	}
}