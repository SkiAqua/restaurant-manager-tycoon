extends Node

const DEBUG_MODE = true
const TILESIZE = 128

const ITEM_IDS = {
	"coffee_capsule": 0,
	"coffee_cup": 1,
	"milk":2
}

func is_debug_mode() -> bool:
	return DEBUG_MODE