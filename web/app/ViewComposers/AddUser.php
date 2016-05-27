<?php

namespace App\ViewComposers;

use Illuminate\View\View;
use Auth;

class AddUser 
{
	public function compose(View $view)
	{
		$view->with('user', auth()->user());
	}
}

// user view composer